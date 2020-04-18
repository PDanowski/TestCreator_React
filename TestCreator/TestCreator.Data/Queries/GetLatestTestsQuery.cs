using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetLatestTestsQuery : IQuery
    {
        public int Number { get; set; }
    }
}
