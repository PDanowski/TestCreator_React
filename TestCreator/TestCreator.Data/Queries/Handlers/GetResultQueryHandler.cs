﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetResultQueryHandler : QueryHandler<GetResultQuery, GetResultQueryResult>
    {
        private readonly IResultDtoConverter _dtoConverter;

        public GetResultQueryHandler(EfDbContext applicationDbContext, IResultDtoConverter dtoConverter) : base(applicationDbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetResultQueryResult Handle(GetResultQuery request)
        {
            var result = DbContext.Results.AsNoTracking().FirstOrDefault(t => t.Id.Equals(request.Id));

            return new GetResultQueryResult
            {
                Result = _dtoConverter.Convert(result)
            };
        }

        protected override async Task<GetResultQueryResult> HandleAsync(GetResultQuery request)
        {
            var result = await DbContext.Results.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(request.Id));

            return new GetResultQueryResult
            {
                Result = _dtoConverter.Convert(result)
            };
        }
    }
}
