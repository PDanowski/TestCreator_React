using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Models.DAO;
using TestCreator.Data.Queries.Consts;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetTestsByParamHandler : QueryHandler<GetTestsByParamQuery, GetTestsQueryResult>
    {
        private readonly ITestDtoConverter _testDtoConverter;

        public GetTestsByParamHandler(EfDbContext applicationDbContext, ITestDtoConverter testDtoConverter) : base(applicationDbContext)
        {
            _testDtoConverter = testDtoConverter;
        }

        protected override GetTestsQueryResult Handle(GetTestsByParamQuery request)
        {
            IQueryable<Test> tests = null;

            switch (request.Param)
            {
                case TestsOrder.ByTitle:
                    tests = DbContext.Tests.OrderBy(t => t.Title);
                    break;
                case TestsOrder.Latest:
                    tests = DbContext.Tests.OrderByDescending(t => t.CreationDate);
                    break;
                case TestsOrder.Random:
                    tests = DbContext.Tests.OrderBy(t => Guid.NewGuid());
                    break;
            }

            return new GetTestsQueryResult
            {
                Tests = _testDtoConverter.Convert(tests?.Take(request.Number).ToList())
            };
        }

        protected override async Task<GetTestsQueryResult> HandleAsync(GetTestsByParamQuery request)
        {
            IQueryable<Test> tests = null;

            switch (request.Param)
            {
                case TestsOrder.ByTitle:
                    tests = DbContext.Tests.OrderBy(t => t.Title);
                    break;
                case TestsOrder.Latest:
                    tests = DbContext.Tests.OrderByDescending(t => t.CreationDate);
                    break;
                case TestsOrder.Random:
                    tests = DbContext.Tests.OrderBy(t => Guid.NewGuid());
                    break;
            }

            return new GetTestsQueryResult
            {
                Tests = _testDtoConverter.Convert(await tests?.Take(request.Number)?.ToListAsync())
            };
        }
    }
}
