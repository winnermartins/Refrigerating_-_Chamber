using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class ThingState
    {
        public List<ThingTelemetry> _telemetry;
        public ThingInfo _deviceInfo;
        public string _deviceGroupId;
    }
}
