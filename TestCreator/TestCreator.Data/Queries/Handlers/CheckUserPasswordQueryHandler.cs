using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Models.DAO;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class CheckUserPasswordQueryHandler : QueryHandler<CheckUserPasswordQuery, CheckUserPasswordQueryResult>
    {
        private readonly IApplicationUserConverter _userConverter;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckUserPasswordQueryHandler(EfDbContext applicationDbContext, 
            UserManager<ApplicationUser> userManager, IApplicationUserConverter userConverter) 
            : base(applicationDbContext)
        {
            _userManager = userManager;
            _userConverter = userConverter;
        }

        protected override CheckUserPasswordQueryResult Handle(CheckUserPasswordQuery request)
        {
            var user = _userConverter.Convert(request.User);
            //TODO: Probably Get() user by id will be necessary, mapped user could be not recognized correctly
            return new CheckUserPasswordQueryResult
            {
                IsCorrect = _userManager.CheckPasswordAsync(user, request.Password).Result
            };
        }

        protected override async Task<CheckUserPasswordQueryResult> HandleAsync(CheckUserPasswordQuery request)
        {
            var user = _userConverter.Convert(request.User);
            //TODO: Probably Get() user by id will be necessary, mapped user could be not recognized correctly
            return new CheckUserPasswordQueryResult
            {
                IsCorrect = await _userManager.CheckPasswordAsync(user, request.Password)
            };
        }
    }
}
