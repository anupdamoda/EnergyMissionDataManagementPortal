using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.Data.Entities
{
    public class Contract
    {
        public int contract_id { get; set; }
        public string contract_number { get; set; }
        public string nmi_number { get; set; }
        public string jurisdiction { get; set; }
        public string ordernumber { get; set; }
        public string paymentreferencenumber { get; set; }
        public string distributor { get; set; }
        public string metertype { get; set; }
        public DateTime startofdelivery { get; set; }
        public string meterserialno { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string lastupdatedby { get; set; }

    }
}
