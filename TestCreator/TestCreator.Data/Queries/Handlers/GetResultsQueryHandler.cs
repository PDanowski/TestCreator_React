﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetResultsQueryHandler : QueryHandler<GetResultsQuery, GetResultsQueryResult>
    {
        private readonly IResultDtoConverter _dtoConverter;

        public GetResultsQueryHandler(EfDbContext applicationDbContext, IResultDtoConverter dtoConverter) : base(applicationDbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetResultsQueryResult Handle(GetResultsQuery request)
        {
            var results = DbContext.Results.AsNoTracking().Where(t => t.TestId.Equals(request.TestId));

            return new GetResultsQueryResult
            {
                Results = _dtoConverter.Convert(results)
            };
        }

        protected override async Task<GetResultsQueryResult> HandleAsync(GetResultsQuery request)
        {
            var results = await DbContext.Results.AsNoTracking().Where(t => t.TestId.Equals(request.TestId)).ToListAsync();

            return new GetResultsQueryResult
            {
                Results = _dtoConverter.Convert(results)
            };
        }
    }
}
