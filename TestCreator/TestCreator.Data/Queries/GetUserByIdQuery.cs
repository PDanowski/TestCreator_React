using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetUserByIdQuery : IQuery
    {
        public string UserId { get; set; }
    }
}
