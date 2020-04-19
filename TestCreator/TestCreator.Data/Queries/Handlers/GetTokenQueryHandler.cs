using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetTokenQueryHandler : QueryHandler<GetTokenQuery, GetTokenQueryResult>
    {
        private readonly ITokenDtoConverter _dtoConverter;

        public GetTokenQueryHandler(EfDbContext applicationDbContext, ITokenDtoConverter dtoConverter) : base(applicationDbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetTokenQueryResult Handle(GetTokenQuery request)
        {
            var token = DbContext.Tokens.FirstOrDefault(t => t.ClientId == request.ClientId && t.Value == request.RefreshToken);

            return new GetTokenQueryResult
            {
                Token = _dtoConverter.Convert(token)
            };
        }

        protected override async Task<GetTokenQueryResult> HandleAsync(GetTokenQuery request)
        {
            var token = await DbContext.Tokens
                .FirstOrDefaultAsync(t => t.ClientId == request.ClientId && t.Value == request.RefreshToken);

            return new GetTokenQueryResult
            {
                Token = _dtoConverter.Convert(token)
            };
        }
    }
}
