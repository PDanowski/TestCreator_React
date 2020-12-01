using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetTestsByKeywordQueryHandler : QueryHandler<GetTestsByKeywordQuery, GetTestsQueryResult>
    {
        private readonly ITestDtoConverter _testDtoConverter;
        public GetTestsByKeywordQueryHandler(EfDbContext applicationDbContext, ITestDtoConverter testDtoConverter) : base(applicationDbContext)
        {
            _testDtoConverter = testDtoConverter;
        }

        protected override GetTestsQueryResult Handle(GetTestsByKeywordQuery request)
        {
            var tests = DbContext.Tests.AsNoTracking().Where(t => t.Title.Contains(request.Keyword))
                .Take(request.Number)
                .ToList();

            return new GetTestsQueryResult
            {
                Tests = _testDtoConverter.Convert(tests)
            };
        }

        protected override async Task<GetTestsQueryResult> HandleAsync(GetTestsByKeywordQuery request)
        {
            var tests = await DbContext.Tests.AsNoTracking().Where(t => t.Title.Contains(request.Keyword))
                .Take(request.Number)
                .ToListAsync();

            return new GetTestsQueryResult
            {
                Tests = _testDtoConverter.Convert(tests)
            };
        }
    }
}
