using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetAnswersQueryHandler : QueryHandler<GetAnswersQuery, GetAnswersQueryResult>
    {
        private IAnswerDtoConverter _dtoConverter;

        public GetAnswersQueryHandler(EfDbContext context, IAnswerDtoConverter dtoConverter) : base(context)
        {
            _dtoConverter = dtoConverter;
        }

        protected override GetAnswersQueryResult Handle(GetAnswersQuery request)
        {
            var answers = DbContext.Answers.Where(t => t.QuestionId.Equals(request.QuestionId)).ToList();

            return new GetAnswersQueryResult
            {
                Answers = _dtoConverter.Convert(answers)
            };
        }

        protected override async Task<GetAnswersQueryResult> HandleAsync(GetAnswersQuery request)
        {
            var answers = await DbContext.Answers.Where(t => t.QuestionId.Equals(request.QuestionId)).ToListAsync();

            return new GetAnswersQueryResult
            {
                Answers = _dtoConverter.Convert(answers)
            };
        }
    }
}
