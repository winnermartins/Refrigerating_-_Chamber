using Domain;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actors.Interfaces
{
    public interface IThingGroup : IActor
    {
        Task RegisterDevice(ThingInfo deviceInfo);
        Task UnregisterDevice(ThingInfo deviceInfo);
        Task SendTelemetryAsync(ThingTelemetry telemetry);
    }
}
