using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Models.DAO;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class CheckUserPasswordQueryHandler : QueryHandler<CheckUserPasswordQuery, CheckUserPasswordQueryResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckUserPasswordQueryHandler(EfDbContext applicationDbContext, 
            UserManager<ApplicationUser> userManager) 
            : base(applicationDbContext)
        {
            _userManager = userManager;
        }

        protected override CheckUserPasswordQueryResult Handle(CheckUserPasswordQuery request)
        {
            var user = DbContext.Users.FirstOrDefault(t => t.Id == request.User.Id);
            return new CheckUserPasswordQueryResult
            {
                IsCorrect = _userManager.CheckPasswordAsync(user, request.Password).Result
            };
        }

        protected override async Task<CheckUserPasswordQueryResult> HandleAsync(CheckUserPasswordQuery request)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(t => t.Id == request.User.Id);
            return new CheckUserPasswordQueryResult
            {
                IsCorrect = await _userManager.CheckPasswordAsync(user, request.Password)
            };
        }
    }
}
