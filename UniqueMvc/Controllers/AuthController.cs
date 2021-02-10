using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniqueMvc.Data;
using UniqueMvc.Models;

namespace UniqueMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName); //name = username

            if (user != null)
            {
                
                //sign in
                var signInResult = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);

                if (signInResult.Succeeded)
                {
                    if(user.Access == "Customer")
                    {
                        return RedirectToAction("Index", "Customer", new { area = "" });
                    }
                }
            }

            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password, string name, string lastName)
        {
            var user = new ApplicationUser
            {
                Name = name,
                Access = "Customer",
                LastName = lastName,
                UserName = userName,
            };

            var result = await userManager.CreateAsync(user, password);
            
            if (result.Succeeded)
            {
                user = await userManager.FindByNameAsync(user.UserName);

                await authDbContext.AddRangeAsync();
                await authDbContext.SaveChangesAsync();

                var cusInfo = new cusInfo
                {
                    AppUserID = user.Id,
                };

                await  authDbContext.AddAsync(cusInfo);
                await authDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Customer", new { area = "" } );
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
