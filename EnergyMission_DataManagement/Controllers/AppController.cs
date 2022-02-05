//using AceSchoolPortal.Data;
//using AceSchoolPortal.Data.Entities;
//using AceSchoolPortal.ViewModels;
using EnergyMission_DataManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AceSchoolPortal.Controllers
{
    public class AppController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;

        public AppController(IDataRepository repository, DataEnergyContext db)
        {
            _repository = repository;
            this.db = db;
        }

        public IActionResult index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult NotEnabled()
        {
            return View();
        }

        public IActionResult NewRegistration()
        {
            return View();
        }
    }
}