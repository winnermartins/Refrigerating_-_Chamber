using Actors.Interfaces;
using Domain;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
    [StatePersistence(StatePersistence.Persisted)]
    public class Thing : Actor, IThing
    {
        private readonly string storageConnectionString = ""; //Entre aqui a sua connection String
        private ThingState State = new ThingState();

        public Thing(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            State._telemetry = new List<ThingTelemetry>();
            State._deviceGroupId = ""; // not activated
            return base.OnActivateAsync();
        }

        public Task SendTelemetryAsync(ThingTelemetry telemetry)
        {
            State._telemetry.Add(telemetry); // saving data at the device level
            if (State._deviceGroupId != "")
            {
                var deviceGroup = ActorProxy.Create<IThingGroup>(new ActorId(State._deviceGroupId));
                return deviceGroup.SendTelemetryAsync(telemetry); // sending telemetry data for aggregation
            }
            return Task.FromResult(true);
        }

        public Task ActivateMeAsync(string PK, string RK, string id, string location, string temperature, string humidity, string current)
        {
            CloudTable cloudTable;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference("RefrigeratingChamber");
            cloudTable.CreateIfNotExistsAsync();

            State._deviceInfo = new ThingInfo()
            {
                DeviceId = id,
                Local = location,
                Temp = temperature,
                Umid = humidity,
                Corr = current,
            };

            var Device = new DeviceEntity(PK, RK)
            {
                id = State._deviceInfo.DeviceId,
                location = State._deviceInfo.Local,
                temperature = State._deviceInfo.Temp,
                humidity = State._deviceInfo.Umid,
                current = State._deviceInfo.Corr,
            };

            

            TableOperation insertOperation = TableOperation.InsertOrReplace(Device);
            cloudTable.ExecuteAsync(insertOperation);

            // based on the info, assign a group... for demonstration we are assigning a random group
            State._deviceGroupId = State._deviceInfo.Local;

            var deviceGroup = ActorProxy.Create<IThingGroup>(new ActorId(State._deviceInfo.Local));
            return deviceGroup.RegisterDevice(State._deviceInfo);
        }
    }
}
