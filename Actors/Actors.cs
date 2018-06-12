using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Actors.Interfaces;

namespace Actors
{
    /// <remarks>
    /// Esta classe representa um ator.
    /// Cada ActorID é mapeada para uma instância desta classe.
    /// O atributo StatePersistence determina persistência e replicação do estado ator:
    ///  - Persistido: o estado é gravado para o disco e replicado.
    ///  - Volátil: o estado é mantido somente na memória e replicado.
    ///  - Nenhum: o estado é mantido somente na memória e não é replicado.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class Actors : Actor, IActors
    {
        /// <summary>
        /// Inicializa uma nova instância de Actors
        /// </summary>
        /// <param name="actorService">O Microsoft.ServiceFabric.Actors.Runtime.ActorService que hospedará esta instância de ator.</param>
        /// <param name="actorId">O Microsoft.ServiceFabric.Actors.ActorId para esta instância de ator.</param>
        public Actors(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// Este método é chamado sempre que um ator é ativado.
        /// Um ator é ativado na primeira vez que algum de seus métodos é invocado.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // O StateManager é o repositório de estado particular deste ator.
            // Os dados armazenados no StateManager serão replicados para alta disponibilidade para os atores que usam armazenamento de estado volátil ou persistente.
            // Qualquer objeto serializado pode ser salvo no StateManager.
            // Para obter mais informações, consulte https://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("count", 0);
        }

        /// <summary>
        /// TODO: substitua pelo seu próprio método de ator.
        /// </summary>
        /// <returns></returns>
        Task<int> IActors.GetCountAsync(CancellationToken cancellationToken)
        {
            return this.StateManager.GetStateAsync<int>("count", cancellationToken);
        }

        /// <summary>
        /// TODO: substitua pelo seu próprio método de ator.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task IActors.SetCountAsync(int count, CancellationToken cancellationToken)
        {
            // Não há garantia de que as solicitações serão processadas em ordem, nem de que serão processadas no máximo uma vez.
            // A função de atualização aqui verifica se a contagem de entrada é maior do que a contagem atual para manter a ordem.
            return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value, cancellationToken);
        }
    }
}
