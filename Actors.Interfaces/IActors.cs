using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace Actors.Interfaces
{
    /// <summary>
    /// Essa interface define os métodos expostos por um ator.
    /// Os clientes usam esta interface para interagir com o ator que a implementa.
    /// </summary>
    public interface IActors : IActor
    {
        /// <summary>
        /// TODO: substitua pelo seu próprio método de ator.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(CancellationToken cancellationToken);

        /// <summary>
        /// TODO: substitua pelo seu próprio método de ator.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task SetCountAsync(int count, CancellationToken cancellationToken);
    }
}
