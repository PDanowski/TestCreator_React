using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetUserByIdQueryHandler : QueryHandler<GetUserByIdQuery, GetUserQueryResult>
    {
        private readonly IApplicationUserDtoConverter _dtoConverter;

        public GetUserByIdQueryHandler(EfDbContext applicationDbContext, IApplicationUserDtoConverter dtoConverter)
            : base(applicationDbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetUserQueryResult Handle(GetUserByIdQuery request)
        {
            var user = DbContext.Users.AsNoTracking().FirstOrDefault(t => t.Id.Equals(request.UserId));

            return new GetUserQueryResult
            {
                User = _dtoConverter.Convert(user)
            };
        }

        protected override async Task<GetUserQueryResult> HandleAsync(GetUserByIdQuery request)
        {
            var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(request.UserId));

            return new GetUserQueryResult
            {
                User = _dtoConverter.Convert(user)
            };
        }
    }
}
