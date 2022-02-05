using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyMission_DataManagement.Data;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnergyMission_DataManagement
{
	public class Startup
	{
		private readonly IConfiguration _config;
		public Startup(IConfiguration config)
		{
			_config = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
			{
				cfg.User.RequireUniqueEmail = true;
				//cfg.Password.RequiredLength = 5;
				//cfg.Password.RequireNonAlphanumeric = true;
				//cfg.Password.RequiredUniqueChars = 2;
			})
			   .AddEntityFrameworkStores<DataEnergyContext>();

			services.AddDbContext<DataEnergyContext>(cfg =>
			{
				cfg.UseSqlServer(_config.GetConnectionString("EnergyMissionConnectionString"));
				
			});
			services.AddTransient<DataSeeder>();

			services.AddScoped<IDataRepository, DataRepository>();

			services.AddRazorPages();
			services.AddControllersWithViews();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("Fallback",
					"/{controller}/{action}/{id?}",
					new { controller = "App", action = "Index" });
			});
		}
	}
}
