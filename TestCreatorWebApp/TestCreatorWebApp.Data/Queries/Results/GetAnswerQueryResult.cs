using TestCreatorWebApp.Data.Models.DTO;
using TestCreatorWebApp.Data.Queries.Interfaces;

namespace TestCreatorWebApp.Data.Queries.Results
{
    public class GetAnswerQueryResult : IQueryResult
    {
        public Answer Answer { get; set; }
    }
}
