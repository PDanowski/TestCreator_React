using Newtonsoft.Json;

namespace TestCreatorWebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel()
        {

        }

        public string Token { get; set; }
        public int Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
