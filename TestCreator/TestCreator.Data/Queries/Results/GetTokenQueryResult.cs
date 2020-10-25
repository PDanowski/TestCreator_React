using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetTokenQueryResult : IQueryResult
    {
        public Token Token { get; set; }
    }
}
