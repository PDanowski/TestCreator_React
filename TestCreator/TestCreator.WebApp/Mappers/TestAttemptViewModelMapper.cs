using System;
using System.Collections.Generic;
using System.Linq;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.Mappers.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Mappers
{
    public class TestAttemptViewModelMapper : ITestAttemptViewModelMapper
    {
        private readonly IAnswerViewModelConverter _answerViewModelConverter;
        private readonly IQuestionViewModelConverter _questionViewModelConverter;

        public TestAttemptViewModelMapper(
            IAnswerViewModelConverter answerViewModelConverter, 
            IQuestionViewModelConverter questionViewModelConverter)
        {
            _answerViewModelConverter = answerViewModelConverter;
            _questionViewModelConverter = questionViewModelConverter;
        }

        public TestAttemptViewModel Convert(GetTestAttemptQueryResult testAttempt)
        {
            if (testAttempt.Test == null)
            {
                return null;
            }

            var viewModel = new TestAttemptViewModel
            {
                TestId = testAttempt.Test.Id,
                Title = testAttempt.Test.Title,
                TestAttemptEntries = new List<TestAttemptEntryViewModel>()
            };

            foreach (var question in testAttempt.Questions)
            {
                viewModel.TestAttemptEntries.Add(new TestAttemptEntryViewModel
                {
                    Question = _questionViewModelConverter.Convert(question),
                    Answers = testAttempt.Answers.Where(a => a.QuestionId == question.Id)
                        .Select(x => _answerViewModelConverter.Convert(x) as TestAttemptAnswerViewModel).ToList()
                });
            }

            return viewModel;
        }
    }
}
