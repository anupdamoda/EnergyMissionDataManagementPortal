using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AceSchoolPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class NMIAPIController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;

        public NMIAPIController(IDataRepository repository, DataEnergyContext db)
        {
            _repository = repository;
            this.db = db;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<NMIs>> NMIAPI()
        {
            try
            {
                return Ok(_repository.GetAllNMIs());
            }
            catch (Exception ex)
            {

                return BadRequest("Failed to get nmis");
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult NMIAPI(NewNMIViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                var newNMI = new NMIs()
                {
                    nmi_number = model.nmi_number,
                    jurisdiction = model.Jusridiction,
                    metertype = model.MeterType,
                    distributor = model.Distributor,
                    meterserialno = model.MeterSerialNumber,
                    nsrd = model.NSRD,
                    usedforcontract = model.UsedForContract,
                    lastupdatedby = "Automation_API",
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _repository.AddEntity(newNMI);
                _repository.SaveAll();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get nmi");
            }
        }
    }
}