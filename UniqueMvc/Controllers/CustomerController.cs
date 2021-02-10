using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniqueMvc.Data;
using UniqueMvc.Models;

namespace UniqueMvc.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly AuthDbContext authDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public CustomerController(AuthDbContext authDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Customer = await userManager.FindByIdAsync(id);
            var cusInfo = await authDbContext.cusInfos.FirstOrDefaultAsync(i => i.AppUserID == id);
            if (cusInfo == null)
            {
                return NotFound();
            }
            cusInfo.ApplicationUser = Customer;
            return View(cusInfo);

        }
    }
}
