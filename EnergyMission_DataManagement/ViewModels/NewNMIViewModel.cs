using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace EnergyMission_DataManagement.ViewModels
{
    public class NewNMIViewModel
    {
        public int NMI_id { get; set; }
        [Required]
        public string NMI { get; set; }
        public string Jusridiction { get; set; }
        public SelectList Juris { get; set; }
        public string SelectedJuris { get; set; }
        public string SelectedMeterType { get; set; }
        public string Distributor { get; set; }
        public string MeterType { get; set; }
        public DateTime NSRD { get; set; }
        public string MeterSerialNumber { get; set; }
        public bool UsedForContract { get; set; }

    }
}