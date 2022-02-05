using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AceSchoolPortal.Controllers
{
    public class UsersController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public object DbContextInMemory { get; private set; }

        public UsersController(IDataRepository repository, DataEnergyContext db, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            this.db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult UserManagement(IdentityUser user)
        {
            var results = _userManager.Users;
            return View(results.ToList());
        }

        public IActionResult Enable(string Id)
        {
            var usr = _repository.GetAllUsers().Where(s => s.Id == Id).FirstOrDefault();
            //var usr = _userManager.Users.Where(s => s.Id == Id).FirstOrDefault();
            return View(usr);
        }

        [HttpPost]
        public async Task<IActionResult> Enable(IdentityUser user)
        {
            var resultuser = _userManager.Users.AsNoTracking().Where(s => s.Id == user.Id).FirstOrDefault();

            resultuser.LockoutEnabled = user.LockoutEnabled;
            await _userManager.UpdateAsync(resultuser);

            return RedirectToAction("UserManagement");
        }
    }
}