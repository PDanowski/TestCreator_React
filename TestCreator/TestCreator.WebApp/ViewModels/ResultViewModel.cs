using System;
using Newtonsoft.Json;

namespace TestCreator.WebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ResultViewModel
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
