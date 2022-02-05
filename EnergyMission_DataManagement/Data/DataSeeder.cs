using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace EnergyMission_DataManagement.Data
{
	public class DataSeeder
	{
        private readonly DataEnergyContext _ctx;
        private readonly IHostEnvironment _hosting;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(DataEnergyContext ctx, IHostEnvironment hosting, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            IdentityUser user = await _userManager.FindByNameAsync("Anup.Damodaran.2@team.telstra.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    Email = "Anup.Damodaran.2@team.telstra.com",
                    UserName = "Anup.Damodaran.2@team.telstra.com"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
            }

            IdentityRole role1 = await _roleManager.FindByNameAsync("Basic");
            if (role1 == null)
            {
                role1 = new IdentityRole()
                {
                    Name = "Basic"
                };
                var result1 = await _roleManager.CreateAsync(role1);
                if (result1 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create role in Seeder");
                }
            }
            

            IdentityRole role2 = await _roleManager.FindByNameAsync("SuperAdmin");
            if (role2 == null)
            {
                role2 = new IdentityRole()
                {
                    Name = "SuperAdmin"
                };
                var result2 = await _roleManager.CreateAsync(role2);
                if (result2 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create role in Seeder");
                }
            }

            IdentityRole role3 = await _roleManager.FindByNameAsync("Administrator");
            if (role3 == null)
            {
                role3 = new IdentityRole()
                {
                    Name = "Administrator"
                };
                var result3 = await _roleManager.CreateAsync(role3);
                if (result3 != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create role in Seeder");
                }
            }
        }
    }

}

