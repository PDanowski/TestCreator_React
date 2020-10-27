using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
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
using TestCreator.WebApp.Services;
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TestAttemptControllerTests
    {
        private TestAttemptController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _controller = new TestAttemptController(
                new TestAttemptViewModelMapper(new AnswerViewModelConverter(), new QuestionViewModelConverter()), 
                new TestResultCalculationService(), 
                _queryDispatcherMock.Object);
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
        }

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

            _queryDispatcherMock.Setup(x => 
                    x.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(It.IsAny<GetTestAttemptQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), queryResult.Test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), queryResult.Test.Id);
            Assert.IsNotNull(result.GetValueFromJsonResult<List<TestAttemptEntryViewModel>>("TestAttemptEntries"));
        }

        [Test]
        public async Task Start_InvalidIdGiven_ReturnsNotFound()
        {
            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(It.IsAny<GetTestAttemptQuery>()))
                .Returns(Task.FromResult(new GetTestAttemptQueryResult
                {
                    Test = null
                }));

            var result = await _controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Post_CorrectTestAttemptViewModel_ReturnsJsonViewModel()
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

            var result = controller.Post(viewModel) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), returnedViewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), returnedViewModel.TestId);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Score"), returnedViewModel.Score);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("MaximalPossibleScore"), returnedViewModel.MaximalPossibleScore);
        }

        [Test]
        public void Post_NullTestAttemptViewModel_ReturnsNotFound()
        {
            var result = _controller.Post(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void Post_CorrectTestAttemptViewModelErrorDuringCalculating_ReturnsNotFound()
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

            var result = controller.Post(viewModel) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
