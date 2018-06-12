using Domain;
using Microsoft.ServiceFabric.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actors.Interfaces
{
    public interface IThing : IActor
    {
        Task ActivateMeAsync(string PK, string RK, string id, string location, string temperature, string humidity, string current);
        Task SendTelemetryAsync(ThingTelemetry telemetry);
    }
}
