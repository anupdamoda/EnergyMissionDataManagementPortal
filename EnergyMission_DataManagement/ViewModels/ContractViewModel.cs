using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.ViewModels
{
    public class NewContractViewModel
    {
        public int Contract_id { get; set; }
        [Required]
        public string Contract_number { get; set; }
        public string NMI_number { get; set; }
        public string OrderNumber { get; set; }
        public DateTime StartOfDeliveryDate { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string Jusridiction { get; set; }
        public string Distributor { get; set; }
        public string MeterType { get; set; }
        public string MeterSerialNumber { get; set; }
        public string MeterReads { get; set; }
    }
}