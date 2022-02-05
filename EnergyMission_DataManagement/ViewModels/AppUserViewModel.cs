using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyMission_DataManagement.ViewModels
{
    public class AppUserViewModel : IdentityUser
    {
        public string UserName { get; set; }
        public bool LockoutEnabled { get; set; }
       
    }
}