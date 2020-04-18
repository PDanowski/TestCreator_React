using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetAnswerQueryHandler : QueryHandler<GetAnswerQuery, GetAnswerQueryResult>
    {
        private IAnswerDtoConverter _dtoConverter;

        public GetAnswerQueryHandler(EfDbContext context, IAnswerDtoConverter dtoConverter) : base(context)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetAnswerQueryResult Handle(GetAnswerQuery request)
        {
            var answer = DbContext.Answers.FirstOrDefault(t => t.Id.Equals(request.Id));

            return new GetAnswerQueryResult
            {
                Answer = _dtoConverter.Convert(answer)
            };
        }

        protected override async Task<GetAnswerQueryResult> HandleAsync(GetAnswerQuery request)
        {
            var answer = await DbContext.Answers.FirstOrDefaultAsync(t => t.Id.Equals(request.Id));

            return new GetAnswerQueryResult
            {
                Answer = _dtoConverter.Convert(answer)
            };
        }
    }
}
