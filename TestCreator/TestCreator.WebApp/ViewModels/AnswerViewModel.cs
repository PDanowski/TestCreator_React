﻿using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace TestCreator.WebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
