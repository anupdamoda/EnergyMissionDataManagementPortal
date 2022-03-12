using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyMission_DataManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnergyMission_DataManagement.Data
{
	public class DataRepository : IDataRepository
	{
        private readonly DataEnergyContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public DataRepository(DataEnergyContext ctx, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
          
        }

        public IEnumerable<NMIs> GetAllNMIs()
        {
            return _ctx.NMIs
                .OrderByDescending(u => u.updated_at)
                .ToList();
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            return _ctx.Contracts
                .OrderBy(u => u.contract_number)
                .ToList();
        }

        public IEnumerable<CustAccntCode> GetAllCACs()
        {
            return _ctx.CACs
                .OrderBy(u => u.CAC_number)
                .ToList();
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _ctx.Users
                .OrderBy(u => u.UserName)
                .ToList();

        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {

            return _ctx.Roles
                .ToList();

        }

        //public IEnumerable<IdentityUserRole<IdentityUser>> GetAllUserRoles()
        //{
        //    return _ctx.UserRoles
        //        .ToArray();
        //}

        public IEnumerable<OperationsHistory> GetAllOpsHists()
        {
            return _ctx.OpsHists
                .OrderByDescending(u => u.updated_at)
                .ToList();
        }
  

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void RemoveEntity(object model)
        {
            _ctx.Remove(model);
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
