using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UniqueMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Access { get; set; }
        public string LastName { get; set; }
    }
}
