using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetAnswersQuery : IQuery
    {
        public int QuestionId { get; set; }
    }
}
