using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyMission_DataManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnergyMission_DataManagement.Data
{
    public interface IDataRepository
    {
        IEnumerable<NMIs> GetAllNMIs();
        IEnumerable<Contract> GetAllContracts();
        IEnumerable<CustAccntCode> GetAllCACs();
        IEnumerable<IdentityUser> GetAllUsers();
        IEnumerable<IdentityRole> GetAllRoles();
        IEnumerable<EnergyMissionConnectionString> GetAllJurisdictions();
        IEnumerable<MeterType> GetAllMeterType();
        IEnumerable<OperationsHistory> GetAllOpsHists();
        //IEnumerable<IdentityUserRole> GetAllUserRoles();
        void AddEntity(object model);
        void RemoveEntity(object model);
        bool SaveAll();
    }
}
