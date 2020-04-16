using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreatorWebApp.Data.Converters.DTO.Interfaces;
using TestCreatorWebApp.Data.Database;
using TestCreatorWebApp.Data.Queries.Handlers.Common;
using TestCreatorWebApp.Data.Queries.Results;

namespace TestCreatorWebApp.Data.Queries.Handlers
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

            var answerDtos = answers.Select(a => _dtoConverter.Convert(a));
            return new GetAnswersQueryResult
            {
                Answers = answerDtos
            };
        }

        protected override async Task<GetAnswersQueryResult> HandleAsync(GetAnswersQuery request)
        {
            var answers = await DbContext.Answers.Where(t => t.QuestionId.Equals(request.QuestionId)).ToListAsync();

            var answerDtos = answers.Select(a => _dtoConverter.Convert(a));
            return new GetAnswersQueryResult
            {
                Answers = answerDtos
            };
        }
    }
}
