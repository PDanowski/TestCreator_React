namespace TestCreator.WebApp.Services.ValueObjects
{
    public struct TokenData
    {
        public string EncodedToken { get; set; }
        public int ExporationTimeInMinutes { get; set; }
    }
}
