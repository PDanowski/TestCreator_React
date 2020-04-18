using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class GetAnswerQueryResult : IQueryResult
    {
        public Answer Answer { get; set; }
    }
}
