using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TestCreatorWebApp.Data.Models.DTO
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public virtual List<Test> Tests { get; set; }
        public virtual List<Token> Tokens { get; set; }
    }
}
