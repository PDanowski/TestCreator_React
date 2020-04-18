using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetTestsByTitleQuery : IQuery
    {
        public int Number { get; set; }
    }
}
