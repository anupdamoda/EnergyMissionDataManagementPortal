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
    public class ContractController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;

        public ContractController(IDataRepository repository, DataEnergyContext db)
        {
            _repository = repository;
            this.db = db;
        }

        public IActionResult NewContract()
        {
            return View();
        }

        public IActionResult ContractManagement(string searchString)
        {

            var results = _repository.GetAllContracts();

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s => s.contract_number.Contains(searchString)
                                           || s.nmi_number.Contains(searchString)
                                           || s.jurisdiction.Contains(searchString)
                                           || s.metertype.Contains(searchString)
                                           /*|| s.ordernumber.Contains(searchString)*/);
            }
            return View(results.ToList());
        }

        [HttpPost]
        public IActionResult NewContract(NewContractViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            if (ModelState.IsValid)
            {
                var newContract = new Contract()
                {
                    contract_number = model.Contract_number,
                    nmi_number = model.NMI_number,
                    jurisdiction = model.Jusridiction,
                    distributor = model.Distributor,
                    metertype = model.MeterType,
                    startofdelivery = model.StartOfDeliveryDate,
                    ordernumber = model.OrderNumber,
                    paymentreferencenumber = model.PaymentReferenceNumber,
                    meterserialno = model.MeterSerialNumber,
                    lastupdatedby = userId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                var newOps = new OperationsHistory()
                {
                    nmi_number = model.Contract_number,
                    operation = "Insert",
                    lastupdatedby = userId,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _repository.AddEntity(newContract);
                _repository.AddEntity(newOps);
                _repository.SaveAll();
                   
            }
            else
            {
                //show errors
            }

            return RedirectToAction("ContractManagement");

        }
        public IActionResult Edit(int id)
        {
            //here, get the user from the database in the real application
            //getting a user from collection for demo purpose
            var usr = _repository.GetAllContracts().Where(s => s.contract_id == id).FirstOrDefault();
            return View(usr);
        }

        [HttpPost]
        public IActionResult Edit(Contract contract)
        {
            //update nmi in DB using EntityFramework in real-life application
            //update list by removing old user and adding updated user for demo purpose
            var resultcontract = _repository.GetAllContracts().Where(s => s.contract_id == contract.contract_id).FirstOrDefault();

            // created date should remain the same
            contract.created_at = resultcontract.created_at;
            // edited/updated date should be updated to current date (date of the edit)
            contract.updated_at = DateTime.Now;
            // This will look for the current user claim ( current logged in user )
            var userId = User.FindFirstValue(ClaimTypes.Name);
            contract.lastupdatedby = userId;
            var newOps = new OperationsHistory()
            {
                contract_number = contract.contract_number,
                operation = "Updated",
                lastupdatedby = userId,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };


            _repository.RemoveEntity(resultcontract);
            _repository.AddEntity(contract);
            _repository.AddEntity(newOps);
            _repository.SaveAll();

            return RedirectToAction("ContractManagement");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id)
        {
            //here, get the student from the database in the real application
            //getting a student from collection for demo purpose
            var sub = _repository.GetAllContracts().Where(s => s.contract_id == id).FirstOrDefault();
            return View(sub);
        }

        [HttpPost]
        public ActionResult Delete(Contract contract)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            var resultcontract = _repository.GetAllContracts().Where(s => s.contract_id == contract.contract_id).FirstOrDefault();
            var userId = User.FindFirstValue(ClaimTypes.Name);

            var newOps = new OperationsHistory()
            {
                nmi_number = contract.nmi_number,
                operation = "Delete",
                lastupdatedby = userId,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            _repository.RemoveEntity(resultcontract);
            _repository.AddEntity(newOps);
            _repository.SaveAll();

            return RedirectToAction("ContractManagement");
        }

    }
}