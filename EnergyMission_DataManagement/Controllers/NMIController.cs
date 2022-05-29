using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace AceSchoolPortal.Controllers
{
    [Authorize]

    public class NMIController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;
        private readonly UserManager<IdentityUser> _userManager;

        public NMIController(IDataRepository repository, DataEnergyContext db, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            this.db = db;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,SuperAdmin")]
        public IActionResult NewNMI()
        {
            #region ViewBag
            List<SelectListItem> Jurisdiction = new List<SelectListItem>() {
        new SelectListItem {
            Text = "VIC", Value = "VIC"
        },
        new SelectListItem {
            Text = "NSW", Value = "NSW"
        },
        new SelectListItem {
            Text = "QLD", Value = "QLD"
        },
        new SelectListItem {
            Text = "SA", Value = "SA"
        },
        new SelectListItem {
            Text = "TAS", Value = "TAS"
        },
        new SelectListItem {
            Text = "NT", Value = "NT"
        },
    };
            ViewBag.jurisdiction = Jurisdiction;
            #endregion

            #region ViewBag
            List<SelectListItem> MeterType = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Smart", Value = "Smart"
        },
        new SelectListItem {
            Text = "Basic", Value = "Basic"
        },
    };
            ViewBag.metertype = MeterType;
            #endregion
            return View();
        }

        public IActionResult NMIManagement(string nmiString, string jurString, string meterTypeString, DateTime nsrdDate, string UsedCheck)
        {

            var results = _repository.GetAllNMIs();

            ViewData["nmiFilter"] = nmiString;
            ViewData["jurisdictionFilter"] = jurString;
            ViewData["meterTypeFilter"] = meterTypeString;
            ViewData["nsrdFilter"] = nsrdDate.Date;
            ViewData["usedFilter"] = UsedCheck;
            if (String.IsNullOrEmpty(nmiString) && String.IsNullOrEmpty(jurString) && String.IsNullOrEmpty(meterTypeString) && nsrdDate != DateTime.MinValue)
            {
                results = results.Where(s => s.nsrd.Date > nsrdDate);
            }
            if (!String.IsNullOrEmpty(nmiString) && String.IsNullOrEmpty(jurString) && String.IsNullOrEmpty(meterTypeString))
            {
                results = results.Where(s => s.nmi_number.Contains(nmiString));
            }
            if (String.IsNullOrEmpty(nmiString) && !String.IsNullOrEmpty(jurString) && String.IsNullOrEmpty(meterTypeString))
            {
                results = results.Where(s => s.jurisdiction.Contains(jurString));
            }
            if (String.IsNullOrEmpty(nmiString) && String.IsNullOrEmpty(jurString) && !String.IsNullOrEmpty(meterTypeString))
            {
                results = results.Where(s => s.metertype.Contains(meterTypeString));
            }
            if (!String.IsNullOrEmpty(nmiString) && !String.IsNullOrEmpty(jurString) && String.IsNullOrEmpty(meterTypeString))
            {
                results = results.Where(s => s.nmi_number.Contains(nmiString)
                                          || s.jurisdiction.Contains(jurString));
            }
            if (String.IsNullOrEmpty(nmiString) && !String.IsNullOrEmpty(jurString) && !String.IsNullOrEmpty(meterTypeString))
            {
                results = results.Where(s => s.jurisdiction.Contains(jurString)
                                          && s.metertype.Contains(meterTypeString));
            }
            if (String.IsNullOrEmpty(nmiString) && String.IsNullOrEmpty(jurString) && String.IsNullOrEmpty(meterTypeString) && !String.IsNullOrEmpty(UsedCheck))
            {
                if (UsedCheck == "True")
                {
                    results = results.Where(s => s.usedforcontract.Equals(true));
                }
                else
                {
                    results = results.Where(s => s.usedforcontract.Equals(false));
                }

            }
            return View(results.ToList());
        }

        [HttpPost]
        public IActionResult NewNMI(NewNMIViewModel model)
        {
            // This will look for the current user claim ( current logged in user )
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var exitNMI = _repository.GetAllNMIs();

            if (ModelState.IsValid)
            {
                var newNMI = new NMIs()
                {
                    nmi_number = model.nmi_number,
                    jurisdiction = model.SelectedJuris,
                    distributor = model.Distributor,
                    metertype = model.SelectedMeterType,
                    nsrd = model.NSRD,
                    meterserialno = model.MeterSerialNumber,
                    usedforcontract = model.UsedForContract,
                    lastupdatedby = userId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                var newOps = new OperationsHistory()
                {
                    nmi_number = model.nmi_number,
                    operation = "Insert",
                    lastupdatedby = userId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                // check if data (NMI) exists in the database
                bool nmiExists = _repository.GetAllNMIs().Any(s => s.nmi_number.Equals(model.nmi_number));

                if (nmiExists)
                {
                    return RedirectToAction("NmiDataExists", "NMI");
                }
                else
                {
                    _repository.AddEntity(newNMI);
                    _repository.AddEntity(newOps);
                    _repository.SaveAll();
                }
            }
            else
            {
                //show errors
            }

            return RedirectToAction("NMIManagement");

        }

        [Authorize(Roles = "Administrator,SuperAdmin")]
        public IActionResult Edit(int id)
        {
            //here, get the user from the database in the real application
            //getting a user from collection for demo purpose
            var usr = _repository.GetAllNMIs().Where(s => s.nmi_id == id).FirstOrDefault();
            return View(usr);
        }

        [HttpPost]
        public IActionResult Edit(NMIs nmi)
        {
            //update nmi in DB using EntityFramework in real-life application
            //update list by removing old user and adding updated user for demo purpose
            var resultnmi = _repository.GetAllNMIs().Where(s => s.nmi_id == nmi.nmi_id).FirstOrDefault();

            // created date should remain the same
            nmi.created_at = resultnmi.created_at;
            // edited/updated date should be updated to current date (date of the edit)
            nmi.updated_at = DateTime.Now;
            // This will look for the current user claim ( current logged in user )
            var userId = User.FindFirstValue(ClaimTypes.Name);
            nmi.lastupdatedby = userId;

            var newOps = new OperationsHistory()
            {
                nmi_number = nmi.nmi_number,
                operation = "Updated",
                lastupdatedby = userId,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };
            _repository.RemoveEntity(resultnmi);
            _repository.AddEntity(nmi);
            _repository.AddEntity(newOps);
            _repository.SaveAll();

            return RedirectToAction("NMIManagement");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id)
        {
            //here, get the student from the database in the real application
            //getting a student from collection for demo purpose
            var sub = _repository.GetAllNMIs().Where(s => s.nmi_id == id).FirstOrDefault();
            return View(sub);
        }

        [HttpPost]
        public ActionResult Delete(NMIs nmi)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            var subject = _repository.GetAllNMIs().Where(s => s.nmi_id == nmi.nmi_id).FirstOrDefault();
            // This will look for the current user claim ( current logged in user )
            var userId = User.FindFirstValue(ClaimTypes.Name);

            var newOps = new OperationsHistory()
            {
                nmi_number = nmi.nmi_number,
                operation = "Delete",
                lastupdatedby = userId,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };
            _repository.RemoveEntity(subject);
            _repository.AddEntity(newOps);
            _repository.SaveAll();

            return RedirectToAction("NMIManagement");
        }

        public IActionResult NmiDataExists()
        {
            return View();
        }

        //public class MyAuthorizeAttribute : AuthorizeAttribute
        //{
        //    protected void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        //    {
        //        var viewResult = new ViewResult
        //        {
        //            ViewName = "~/Views/Account/Login.cshtml"
        //        };
        //        //filterContext.Result = viewResult;
        //    }
        //}
    }
}