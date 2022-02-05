using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.ViewModels
{
    public class CACViewModel
    {
        public int CAC_id { get; set; }
        [Required]
        public string CAC_number { get; set; }
        public string NMI_number { get; set; }
        public bool UsedForContract { get; set; }
    }
}