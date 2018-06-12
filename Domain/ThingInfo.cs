using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class ThingInfo
    {
        public string DeviceId { get; set; }
        public string Local { get; set; }
        public string Temp { get; set; }
        public string Umid { get; set; }
        public string Corr { get; set; }
        
    }
}
