using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries.Interfaces;

namespace TestCreator.Data.Queries
{
    public class CheckUserPasswordQuery : IQuery
    {
        public ApplicationUser User { get; set; }
        public string Password { get; set; }
    }
}
