using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetQuestionQueryHandler : QueryHandler<GetQuestionQuery, GetQuestionQueryResult>
    {
        private readonly IQuestionDtoConverter _dtoConverter;

        public GetQuestionQueryHandler(EfDbContext dbContext, IQuestionDtoConverter dtoConverter) : base(dbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetQuestionQueryResult Handle(GetQuestionQuery request)
        {
            var question = DbContext.Questions.AsNoTracking().FirstOrDefault(q => q.Id.Equals(request.Id));

            return new GetQuestionQueryResult
            {
                Question = _dtoConverter.Convert(question)
            };
        }

        protected override async Task<GetQuestionQueryResult> HandleAsync(GetQuestionQuery request)
        {
            var question = await DbContext.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id.Equals(request.Id));

            return new GetQuestionQueryResult
            {
                Question = _dtoConverter.Convert(question)
            };
        }
    }
}
