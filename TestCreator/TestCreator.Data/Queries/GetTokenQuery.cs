using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetTokenQuery : IQuery
    {
        public string ClientId { get; set; }
        public string RefreshToken { get; set; }
    }
}
