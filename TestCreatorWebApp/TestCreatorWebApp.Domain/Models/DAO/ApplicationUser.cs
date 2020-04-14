using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCreatorWebApp.Data.Models.DAO
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
                
        }

        public string DisplayName { get; set; }
        public string Notes { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int Flags { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastModificationDate { get; set; }

        public virtual List<Test> Tests { get; set; }
        public virtual List<Token> Tokens { get; set; }
    }
}
