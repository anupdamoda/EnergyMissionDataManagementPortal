using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnergyMission_DataManagement.Data
{
    public class DataEnergyContext : IdentityDbContext<IdentityUser>
    {
        public DataEnergyContext(DbContextOptions<DataEnergyContext> options) : base(options)
        {

        }

        public DbSet<NMIs> NMIs { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CustAccntCode> CACs { get; set; }
        public DbSet<OperationsHistory> OpsHists { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NMIs>()
                .HasKey(u => new { u.nmi_id });
            modelBuilder.Entity<Contract>()
                .HasKey(h => new { h.contract_id });
            modelBuilder.Entity<CustAccntCode>()
                .HasKey(h => new { h.CAC_id });
            modelBuilder.Entity<OperationsHistory>()
                .HasKey(h => new { h.operation_id });
        }
    }
}
