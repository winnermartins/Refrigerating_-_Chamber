using System;

namespace Domain
{
    [Serializable]
    public class ThingTelemetry
    {
        public bool DevelopedFault { get; set; }
        public string Region { get; set; }
        public string DeviceId { get; set; }
    }
}
