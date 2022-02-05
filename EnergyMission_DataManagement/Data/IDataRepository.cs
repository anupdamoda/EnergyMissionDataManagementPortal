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
        IEnumerable<OperationsHistory> GetAllOpsHists();
        void AddEntity(object model);
        void RemoveEntity(object model);
        bool SaveAll();
    }
}
