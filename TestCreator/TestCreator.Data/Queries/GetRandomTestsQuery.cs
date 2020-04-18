using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetRandomTestsQuery : IQuery
    {
        public int Number { get; set; }
    }
}
