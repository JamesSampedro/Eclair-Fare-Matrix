using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueMvc.Models
{
    public class cusInfo
    {
        [Key]
        public int ID { get; set; }
        public string AppUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
