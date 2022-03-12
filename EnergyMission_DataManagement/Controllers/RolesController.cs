using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace AceSchoolPortal.Controllers
{
    public class RolesController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private EnergyMission_DataManagement.ViewModels.ApplicationDbContext context;
        public object DbContextInMemory { get; private set; }

        public RolesController(IDataRepository repository, DataEnergyContext db, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            this.db = db;
            _userManager = userManager;
            context = new EnergyMission_DataManagement.ViewModels.ApplicationDbContext();
        }

        [HttpGet]
        public IActionResult RolesManagement()
        {

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UsersinRolesViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);

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