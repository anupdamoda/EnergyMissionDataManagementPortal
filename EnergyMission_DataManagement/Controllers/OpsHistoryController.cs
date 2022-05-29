using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using EnergyMission_DataManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace AceSchoolPortal.Controllers
{
    public class OpsHistoryController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public OpsHistoryController(IDataRepository repository, DataEnergyContext db, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            this.db = db;
            _userManager = userManager;
        }

        public IActionResult OpsHistManagement(String searchString)
        {

            var results = _repository.GetAllOpsHists();

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s => s.nmi_number.Contains(searchString)
                                           || s.cac_number.Contains(searchString)
                                           || s.contract_number.Contains(searchString)
                                           || s.lastupdatedby.Contains(searchString));
            }
            return View(results.ToList());
        }

        [HttpPost]
        public IActionResult NewNMI(NewNMIViewModel model)
        {
            // This will look for the current user claim ( current logged in user )
            var userId = User.FindFirstValue(ClaimTypes.Name);
            
            if (ModelState.IsValid)
            {
                var newNMI = new NMIs()
                {
                    nmi_number = model.nmi_number,
                    jurisdiction = model.Jusridiction,
                    distributor = model.Distributor,
                    metertype = model.MeterType,
                    nsrd = model.NSRD,
                    meterserialno = model.MeterSerialNumber,
                    usedforcontract = model.UsedForContract,
                    lastupdatedby = userId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _repository.AddEntity(newNMI);
                _repository.SaveAll();
            }
            else
            {
                //show errors
            }

            return RedirectToAction("NMIManagement");

        }
    }
}