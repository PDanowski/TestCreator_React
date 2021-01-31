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
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TestAttemptControllerTests
    {
        private TestAttemptController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ITestResultCalculationService> _testResultCalculationServiceMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _testResultCalculationServiceMock = new Mock<ITestResultCalculationService>();
            _controller = new TestAttemptController(
                new TestAttemptViewModelMapper(new TestAttemptAnswerViewModelConverter(), new QuestionViewModelConverter()),
                _testResultCalculationServiceMock.Object, 
                _queryDispatcherMock.Object);
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
        }

        [Test]
        public async Task Get_WhenCorrectIdGiven_ShouldReturnJsonViewModel()
        {
            //Arrange
            var queryResult = new GetTestAttemptQueryResult
            {
                Test = _fixture.Create<Test>(),
                Answers = new List<Answer>(),
                Questions = new List<Question>()
            };

            _queryDispatcherMock.Setup(x => 
                    x.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(It.IsAny<GetTestAttemptQuery>()))
                .Returns(Task.FromResult(queryResult));

            //Act
            var result = await _controller.Get(1) as JsonResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), queryResult.Test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), queryResult.Test.Id);
            Assert.IsNotNull(result.GetValueFromJsonResult<List<TestAttemptEntryViewModel>>("TestAttemptEntries"));
        }

        [Test]
        public async Task Get_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            //Arrange
            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(It.IsAny<GetTestAttemptQuery>()))
                .Returns(Task.FromResult(new GetTestAttemptQueryResult
                {
                    Test = null
                }));

            //Act
            var result = await _controller.Get(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void CalculateResult_WhenCorrectTestAttemptViewModel_ShouldReturnsJsonViewModel()
        {
            //Arrange
            var viewModel = _fixture.Create<TestAttemptViewModel>();

            var returnedViewModel = new TestAttemptResultViewModel()
            {
                TestId = 1,
                Title = "title1",
                Score = 10,
                MaximalPossibleScore = 15
            };

            _testResultCalculationServiceMock.Setup(x => x.CalculateResult(It.IsAny<TestAttemptViewModel>())).Returns(returnedViewModel);

            //Act
            var result = _controller.CalculateResult(viewModel) as JsonResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), returnedViewModel.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("TestId"), returnedViewModel.TestId);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Score"), returnedViewModel.Score);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("MaximalPossibleScore"), returnedViewModel.MaximalPossibleScore);
        }

        [Test]
        public void CalculateResult_WhenNullTestAttemptViewModel_ShouldReturnNotFound()
        {
            //Act
            var result = _controller.CalculateResult(null);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void CalculateResult_WhenCorrectTestAttemptViewModelIsGivenButErrorDuringCalculating_ShouldReturnsNotFound()
        {
            //Arrange
            var viewModel = _fixture.Create<TestAttemptViewModel>();

            _testResultCalculationServiceMock.Setup(x => x.CalculateResult(It.IsAny<TestAttemptViewModel>())).Returns<TestAttemptResultViewModel>(null);

            //Act
            var result = _controller.CalculateResult(viewModel) as StatusCodeResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
