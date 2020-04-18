using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetUserByEmailQuery : IQuery
    {
        public string Email { get; set; }
    }
}
