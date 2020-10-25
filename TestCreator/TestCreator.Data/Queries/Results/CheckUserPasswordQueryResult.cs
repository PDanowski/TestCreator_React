using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries.Results
{
    public class CheckUserPasswordQueryResult : IQueryResult
    {
        public bool IsCorrect { get; set; }
    }
}
