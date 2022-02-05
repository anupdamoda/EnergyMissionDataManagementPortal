using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AceSchoolPortal.Controllers
{

    public class CACController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;

        public CACController(IDataRepository repository, DataEnergyContext db)
        {
            _repository = repository;
            this.db = db;
        }

        public IActionResult NewCAC()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult CACManagement(string searchString)
        {

            var results = _repository.GetAllCACs();

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s => s.CAC_number.Contains(searchString));
            }
            return View(results.ToList());
        }

        [HttpPost]
        public IActionResult NewCAC(CACViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (ModelState.IsValid)
            {
                var newCAC = new CustAccntCode()
                {
                    CAC_number = model.CAC_number,
                    nmi_number = model.NMI_number,
                    UsedForContract = model.UsedForContract,                
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                var newOps = new OperationsHistory()
                {
                    nmi_number = model.CAC_number,
                    operation = "Insert",
                    lastupdatedby = userId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                _repository.AddEntity(newCAC);
                _repository.AddEntity(newOps);
                _repository.SaveAll();
            }
            else
            {
                //show errors
            }

            return RedirectToAction("CACManagement");

        }
        public IActionResult Edit(int id)
        {
            //here, get the user from the database in the real application
            //getting a user from collection for demo purpose
            var usr = _repository.GetAllCACs().Where(s => s.CAC_id == id).FirstOrDefault();
            return View(usr);
        }

        [HttpPost]
        public IActionResult Edit(CustAccntCode cac)
        {
            //update nmi in DB using EntityFramework in real-life application
            //update list by removing old user and adding updated user for demo purpose
            var resultcac = _repository.GetAllCACs().Where(s => s.CAC_id == cac.CAC_id).FirstOrDefault();

            // created date should remain the same
            cac.created_at = resultcac.created_at;
            //// edited/updated date should be updated to current date (date of the edit)
            cac.updated_at = DateTime.Now;
            //// This will look for the current user claim ( current logged in user )
            var userId = User.FindFirstValue(ClaimTypes.Name);
            cac.lastupdatedby = userId;
            var newOps = new OperationsHistory()
            {
                cac_number = cac.CAC_number,
                operation = "Updated",
                lastupdatedby = userId,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            _repository.RemoveEntity(resultcac);
            _repository.AddEntity(cac);
            _repository.AddEntity(newOps);
            _repository.SaveAll();

            return RedirectToAction("CACManagement");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id)
        {
            //here, get the student from the database in the real application
            //getting a student from collection for demo purpose
            var sub = _repository.GetAllCACs().Where(s => s.CAC_id == id).FirstOrDefault();
            return View(sub);
        }

        [HttpPost]
        public ActionResult Delete(CustAccntCode cac)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            var resultcac = _repository.GetAllCACs().Where(s => s.CAC_id == cac.CAC_id).FirstOrDefault();
            var userId = User.FindFirstValue(ClaimTypes.Name);

            var newOps = new OperationsHistory()
            {
                cac_number = cac.CAC_number,
                operation = "Delete",
                lastupdatedby = userId,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            _repository.RemoveEntity(resultcac);
            _repository.AddEntity(newOps);
            _repository.SaveAll();

            return RedirectToAction("CACManagement");
        }


    }
}