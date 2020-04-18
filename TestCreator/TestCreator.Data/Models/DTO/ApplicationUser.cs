using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TestCreator.Data.Models.DTO
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
