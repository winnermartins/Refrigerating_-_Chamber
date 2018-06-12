using Actors.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClienteIotExercice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                IList<Task> taks = new List<Task>();
                    for (int i = 0; i < 1000000; i++)
                    {
                       taks.Add(Test(i));
                    }
                Task.WhenAll(taks);

                //var thing = ActorProxy.Create<IThing>(new ActorId(1), new Uri("fabric:/IotExercice/ThingActorService"));
                //thing.ActivateMe("sudeste", 1).Wait();
                //thing.SendTelemetryAsync(new Domain.ThingTelemetry() { DevelopedFault = true, DeviceId = "1", Region = "sudeste" }).Wait();
            }
            catch(Exception e)
            {

            }
        }

        private static async Task Test(int i)
        {
            CancellationToken cancellationToken;
            var actor = ActorProxy.Create<IActors>(new ActorId(i), new Uri("fabric:/IotExercice/ActorsActorService"));
            await actor.SetCountAsync(i, cancellationToken);
            var bla = await actor.GetCountAsync(cancellationToken);
            Console.WriteLine(bla);
        }
    }
}
