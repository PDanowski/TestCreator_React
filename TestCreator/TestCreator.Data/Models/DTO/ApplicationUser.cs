using System;

namespace TestCreator.Data.Models.DTO
{
    public class ApplicationUser 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
