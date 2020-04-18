using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class GetUserByNameQuery : IQuery
    {
        public string UserName { get; set; }
    }
}
