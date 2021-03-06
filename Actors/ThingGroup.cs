﻿using Actors.Interfaces;
using Domain;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
    [StatePersistence(StatePersistence.Persisted)]
    public class ThingGroup : Actor, IThingGroup
    {
        private ThingGroupState State = new ThingGroupState();
        public ThingGroup(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            State._devices = new List<ThingInfo>();
            State._faultsPerRegion = new Dictionary<string, int>();
            State._faultyDevices = new List<ThingInfo>();

            return base.OnActivateAsync();
        }

        public Task RegisterDevice(ThingInfo deviceInfo)
        {
            State._devices.Add(deviceInfo);
            return Task.FromResult(true);
        }

        public Task UnregisterDevice(ThingInfo deviceInfo)
        {
            State._devices.Remove(deviceInfo);
            return Task.FromResult(true);
        }

        public Task SendTelemetryAsync(ThingTelemetry telemetry)
        {
            if (telemetry.DevelopedFault)
            {
                if (false == State._faultsPerRegion.ContainsKey(telemetry.Region))
                {
                    State._faultsPerRegion[telemetry.Region] = 0;
                }
                State._faultsPerRegion[telemetry.Region]++;
                State._faultyDevices.Add(State._devices.Where(d => d.DeviceId == telemetry.DeviceId).FirstOrDefault());

                if (State._faultsPerRegion[telemetry.Region] > State._devices.Count(d => d.Local == telemetry.Region) / 3)
                {
                    Console.WriteLine("Sending an engineer to repair/replace devices in {0}", telemetry.Region);
                    foreach (var device in State._faultyDevices.Where(d => d.Local == telemetry.Region).ToList())
                    {
                        Console.WriteLine("\t{0}", device);
                    }
                }
            }

            return Task.FromResult(true);
        }
    }
}
