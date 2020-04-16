using System;

namespace TestCreatorWebApp.Data.Models.DTO
{
    public class Token
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int Type { get; set; }
        public string UserId { get; set; }
        public string Value { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
