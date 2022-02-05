using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.Data.Entities
{
    public class NMIs
    {
        public int nmi_id { get; set; }
        public string nmi_number { get; set; }
        public string jurisdiction { get; set; }
        public string distributor { get; set; }
        public string metertype { get; set; }
        public DateTime nsrd { get; set; }
        public string lastupdatedby { get; set; }
        public string meterserialno { get; set; }
        public bool usedforcontract { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }
}
