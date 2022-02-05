using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.Data.Entities
{
    public class OperationsHistory
    {
        public int operation_id { get; set; }
        public string nmi_number { get; set; }
        public string contract_number { get; set; }
        public string cac_number { get; set; }
        public string operation { get; set; }
        public string lastupdatedby {get; set;}
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }
}
