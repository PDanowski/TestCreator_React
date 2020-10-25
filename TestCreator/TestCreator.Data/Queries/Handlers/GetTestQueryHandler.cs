using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetTestQueryHandler : QueryHandler<GetTestQuery, GetTestQueryResult>
    {
        private readonly ITestDtoConverter _dtoConverter;

        public GetTestQueryHandler(EfDbContext applicationDbContext, ITestDtoConverter dtoConverter) : base(applicationDbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetTestQueryResult Handle(GetTestQuery request)
        {
            var test = DbContext.Tests.FirstOrDefault(t => t.Id.Equals(request.Id));

            return new GetTestQueryResult
            {
                Test = _dtoConverter.Convert(test)
            };
        }

        protected override async Task<GetTestQueryResult> HandleAsync(GetTestQuery request)
        {
            var test = await DbContext.Tests.FirstOrDefaultAsync(t => t.Id.Equals(request.Id));

            return new GetTestQueryResult
            {
                Test = _dtoConverter.Convert(test)
            };
        }
    }
}
