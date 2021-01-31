using Newtonsoft.Json;

namespace TestCreator.WebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TestAttemptAnswerViewModel : AnswerViewModel
    {
        public bool Checked { get; set; }
    }
}
