using System;

namespace TestCreator.Data.Models.DTO
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public int ViewCount { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }

    }
}
