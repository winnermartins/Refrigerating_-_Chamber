using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class ThingGroupState
    {
        public List<ThingInfo> _devices;
        public Dictionary<string, int> _faultsPerRegion;
        public List<ThingInfo> _faultyDevices;
    }
}
