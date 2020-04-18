using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetQuestionsQuery : IQuery
    {
        public int TestId { get; set; }
    }
}
