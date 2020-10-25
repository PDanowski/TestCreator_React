using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetUserByEmailQueryHandler : QueryHandler<GetUserByEmailQuery, GetUserQueryResult>
    {
        private readonly IApplicationUserDtoConverter _dtoConverter;

        public GetUserByEmailQueryHandler(EfDbContext applicationDbContext, IApplicationUserDtoConverter dtoConverter) 
            : base(applicationDbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetUserQueryResult Handle(GetUserByEmailQuery request)
        {
            var user = DbContext.Users.FirstOrDefault(t => t.Email == request.Email);

            return new GetUserQueryResult
            {
                User = _dtoConverter.Convert(user)
            };
        }

        protected override async Task<GetUserQueryResult> HandleAsync(GetUserByEmailQuery request)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(t => t.Email == request.Email);

            return new GetUserQueryResult
            {
                User = _dtoConverter.Convert(user)
            };
        }
    }
}
