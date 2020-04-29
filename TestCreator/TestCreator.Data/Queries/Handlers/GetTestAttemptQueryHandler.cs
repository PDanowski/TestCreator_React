using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestCreator.Data.Converters.DTO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Models.DAO;
using TestCreator.Data.Queries.Handlers.Common;
using TestCreator.Data.Queries.Results;

namespace TestCreator.Data.Queries.Handlers
{
    public class GetTestAttemptQueryHandler : QueryHandler<GetTestAttemptQuery, GetTestAttemptQueryResult>
    {
        private readonly IAnswerDtoConverter _answerDtoConverter;
        private readonly ITestDtoConverter _testDtoConverter;
        private readonly IQuestionDtoConverter _questionDtoConverter;

        public GetTestAttemptQueryHandler(EfDbContext dbContext, 
            IAnswerDtoConverter answerDtoConverter, 
            IQuestionDtoConverter questionDtoConverter, 
            ITestDtoConverter testDtoConverter) : base(dbContext)
        {
            _answerDtoConverter = answerDtoConverter;
            _questionDtoConverter = questionDtoConverter;
            _testDtoConverter = testDtoConverter;
        }

        protected override GetTestAttemptQueryResult Handle(GetTestAttemptQuery request)
        {
            var test = DbContext.Tests.Where(t => t.Id.Equals(request.Id))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault();

            return new GetTestAttemptQueryResult
            {
                Test = _testDtoConverter.Convert(test),
                Questions = test?.Questions.Select(q => _questionDtoConverter.Convert(q)),
                Answers = test?.Questions.SelectMany(q => q.Answers.Select(a => _answerDtoConverter.Convert(a)))
            };
        }

        protected override async Task<GetTestAttemptQueryResult> HandleAsync(GetTestAttemptQuery request)
        {
            var test = await DbContext.Tests.Where(t => t.Id.Equals(request.Id))
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync();

            return new GetTestAttemptQueryResult
            {
                Test = _testDtoConverter.Convert(test),
                Questions = test?.Questions.Select(q => _questionDtoConverter.Convert(q)),
                Answers = test?.Questions.SelectMany(q => q.Answers.Select(a => _answerDtoConverter.Convert(a)))
            };
        }
    }
}
