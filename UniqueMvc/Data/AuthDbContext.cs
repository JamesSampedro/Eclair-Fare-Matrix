using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniqueMvc.Models;

namespace UniqueMvc.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext( DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<cusInfo> cusInfos { get; set; }


        
    }

}
