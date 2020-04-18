using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetTestsByKeywordQuery : IQuery
    {
        public int Number { get; set; }
        public string Keyword { get; set; }
    }
}
