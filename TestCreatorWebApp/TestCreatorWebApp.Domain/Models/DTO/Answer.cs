﻿using System;

namespace TestCreatorWebApp.Domain.Models.DTO
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public int Type { get; set; }
        public int Flags { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public virtual Question Question { get; set; }
    }
}
