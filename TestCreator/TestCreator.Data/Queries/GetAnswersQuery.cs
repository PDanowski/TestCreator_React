using TestCreatorWebApp.Data.Queries.Interfaces;

namespace TestCreatorWebApp.Data.Queries
{
    public class GetAnswersQuery : IQuery
    {
        public int QuestionId { get; set; }
    }
}
