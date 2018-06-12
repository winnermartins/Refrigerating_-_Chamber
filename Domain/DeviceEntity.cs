using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DeviceEntity : TableEntity
    {
        public DeviceEntity(String PK, String RK)
        {
            PartitionKey = PK; ;
            RowKey = RK;

        }

        public string id { get; set; }
        public string location { get; set; }
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string current { get; set; }
    }
}
