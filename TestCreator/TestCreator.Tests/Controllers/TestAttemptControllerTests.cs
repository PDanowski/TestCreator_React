using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Converters.ViewModel;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.Mappers;
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TestAttemptControllerTests
    {
        [Test]
        public async Task Start_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetTestAttemptQueryResult
            {
                Test = new Test
                {
                    Id = 1,
                    Title = "title1"
                },
                Answers = new List<Answer>(),
                Questions = new List<Question>()
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x => 
                    x.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(It.IsAny<GetTestAttemptQuery>()))
                .Returns(Task.FromResult(queryResult));

            var controller = new TestAttemptController(new TestAttemptViewModelMapper(
                    new AnswerViewModelConverter(), 
                    new QuestionViewModelConverter()), 
                null, mockQuery.Object);

            var result = await controller.GetAsync(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), queryResult.Test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), queryResult.Test.Id);
            Assert.IsNotNull(result.GetValueFromJsonResult<List<TestAttemptEntryViewModel>>("TestAttemptEntries"));
        }

        [Test]
        public async Task Start_InvalidIdGiven_ReturnsNotFound()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(It.IsAny<GetTestAttemptQuery>()))
                .Returns(Task.FromResult(new GetTestAttemptQueryResult
                {
                    Test = null
                }));

            var controller = new TestAttemptController(new TestAttemptViewModelMapper(
                    new AnswerViewModelConverter(), 
                    new QuestionViewModelConverter()),
                null, mockQuery.Object);

            var result = await controller.GetAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Result_CorrectTestAttemptViewModel_ReturnsJsonViewModel()
        {
            var viewModel = new TestAttemptViewModel()
            {
                TestId = 1,
                Title = "title1",
                TestAttemptEntries = new List<TestAttemptEntryViewModel>
                {
                    new TestAttemptEntryViewModel()
                }
            };

            var returnedViewModel = new TestAttemptResultViewModel()
            {
                TestId = 1,
                Title = "title1",
                Score = 10,
                MaximalPossibleScore = 15
            };

            var mockService = new Mock<ITestResultCalculationService>();
            mockService.Setup(x => x.CalculateResult(It.IsAny<TestAttemptViewModel>())).Returns(returnedViewModel);

            var controller = new TestAttemptController(null, mockService.Object, null);

            var result = controller.GetResult(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), returnedViewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), returnedViewModel.TestId);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Score"), returnedViewModel.Score);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("MaximalPossibleScore"), returnedViewModel.MaximalPossibleScore);
        }

        [Test]
        public void Result_NullTestAttemptViewModel_ReturnsNotFound()
        {
            var controller = new TestAttemptController(null, null, null);

            var result = controller.GetResult(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Result_CorrectTestAttemptViewModelErrorDuringCalculating_ReturnsNotFound()
        {
            var viewModel = new TestAttemptViewModel()
            {
                TestId = 1,
                Title = "title1",
                TestAttemptEntries = new List<TestAttemptEntryViewModel>
                {
                    new TestAttemptEntryViewModel()
                }
            };

            var mockService = new Mock<ITestResultCalculationService>();
            mockService.Setup(x => x.CalculateResult(It.IsAny<TestAttemptViewModel>())).Returns<TestAttemptResultViewModel>(null);

            var controller = new TestAttemptController(null, mockService.Object, null);

            var result = controller.GetResult(viewModel) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
