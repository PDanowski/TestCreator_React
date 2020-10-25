using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetQuestionsQueryHandler : QueryHandler<GetQuestionsQuery, GetQuestionsQueryResult>
    {
        private readonly IQuestionDtoConverter _dtoConverter;

        public GetQuestionsQueryHandler(EfDbContext dbContext, IQuestionDtoConverter dtoConverter) : base(dbContext)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetQuestionsQueryResult Handle(GetQuestionsQuery request)
        {
            var questions = DbContext.Questions.Where(q => q.TestId.Equals(request.TestId));

            return new GetQuestionsQueryResult
            {
                Questions = _dtoConverter.Convert(questions)
            };
        }

        protected override async Task<GetQuestionsQueryResult> HandleAsync(GetQuestionsQuery request)
        {
            var questions = await DbContext.Questions.Where(q => q.TestId.Equals(request.TestId)).ToListAsync();

            return new GetQuestionsQueryResult
            {
                Questions = _dtoConverter.Convert(questions)
            };
        }
    }
}
