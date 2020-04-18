using System;
using System.Collections.Generic;
using System.Text;
using TestCreatorWebApp.Data.Models.DTO;

namespace TestCreatorWebApp.Data.Queries
{
    public class CheckUserPasswordQuery
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
    }
}
