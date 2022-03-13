using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyMission_DataManagement.ViewModels;
using EnergyMission_DataManagement.Data.Entities;
using EnergyMission_DataManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EnergyMission_DataManagement.Data;
using Microsoft.EntityFrameworkCore;
using static Fluent.Infrastructure.FluentController.ManageController;
using Microsoft.AspNet.Identity;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;
//using Microsoft.AspNet.Identity;

namespace EnergyMission_DataManagement.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly IDataRepository _repository;


        public AccountController(ILogger<AccountController> logger,
            SignInManager<IdentityUser> signInManager,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            IConfiguration config)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.Name);
                return RedirectToAction("Home", "App", userId);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username,
                    model.Password,
                    model.RememberMe,
                    false);

                var user = await _userManager.FindByEmailAsync(model.Username);

                if (result.Succeeded)
                {
                    //once signed in 
                    if (user.LockoutEnabled == false)
                    {
                        //once signed in and enabled
                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            return Redirect(Request.Query["ReturnUrl"].First());
                        }
                        else
                        {
                            return RedirectToAction("Home", "App");
                        }
                    }
                    else
                    {
                        //once signed in and not enabled
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("NotEnabled", "App");
                    }
                }
            }
            ModelState.AddModelError("", "Entered username or password is wrong");
            return View();
        }

        public IActionResult NewRegistration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewRegistration(IdentityUser user, NewRestgnViewModel model)
        {
           
            user.LockoutEnabled = true;
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Basic");
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create new user in Seeder");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }

        public async Task<IActionResult> MyProfileAsync(string Id)
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

       
        //// Get View using this Method
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        //Save Details of New Password using Identity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model, IdentityUser user)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultuser = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(resultuser, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user1 = await _userManager.FindByIdAsync(user.Id);
                if (user1 != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("PwdChangedSuccess","Account");
            }
            
            return View(model);
        }
        public ActionResult PwdChangedSuccess()
        {
            return View();
        }

    }
}