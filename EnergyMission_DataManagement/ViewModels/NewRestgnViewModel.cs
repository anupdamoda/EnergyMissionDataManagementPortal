using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.ViewModels
{
    public class NewRestgnViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsAdmin { get; set; }
    }
}