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
    public class CACAPIController : ControllerBase
    {
        private readonly IDataRepository _repository;
        private readonly DataEnergyContext db;

        public CACAPIController(IDataRepository repository, DataEnergyContext db)
        {
            _repository = repository;
            this.db = db;
        }
        
        [System.Web.Mvc.HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<CustAccntCode>> CACAPI()
        {
            try
            {
                return Ok(_repository.GetAllCACs());
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get cacs");
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult CACAPI(CACViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                var newCAC = new CustAccntCode()
                {
                    CAC_number = model.CAC_number,
                    nmi_number = model.NMI_number,
                    UsedForContract = model.UsedForContract,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                _repository.AddEntity(newCAC);
                _repository.SaveAll();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get cacs");
            }
        }
    }
}