using System;

namespace TestCreator.Data.Models.DTO
{
    public class Result
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
