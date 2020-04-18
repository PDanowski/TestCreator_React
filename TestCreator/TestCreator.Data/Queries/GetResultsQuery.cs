using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetResultsQuery : IQuery
    {
        public int TestId { get; set; }
    }
}
