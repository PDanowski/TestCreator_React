using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetQuestionQueryResult : IQueryResult
    {
        public Question Question { get; set; }
    }
}
