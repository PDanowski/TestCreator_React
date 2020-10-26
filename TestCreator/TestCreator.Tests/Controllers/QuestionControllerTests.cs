using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Commands;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Converters.DTO;
using TestCreator.WebApp.Converters.ViewModel;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class QuestionControllerTests
    {
        private QuestionController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization()); 
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _controller = new QuestionController(_queryDispatcherMock.Object, _commandDispatcherMock.Object, new QuestionViewModelConverter(), new QuestionDtoConverter());
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
            _commandDispatcherMock.Reset();
        }

        [Test]
        public async Task Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = _fixture.Create<GetQuestionQueryResult>(); 

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionQuery, GetQuestionQueryResult>(It.IsAny<GetQuestionQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.Get(queryResult.Question.Id) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, queryResult.Question.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, queryResult.Question.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            var questionId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionQuery, GetQuestionQueryResult>(It.IsAny<GetQuestionQuery>()))
                .Returns(Task.FromResult(new GetQuestionQueryResult()));

            var result = await _controller.Get(questionId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetByTestId_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = _fixture.Create<GetQuestionsQueryResult>();
            var testId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionsQuery, GetQuestionsQueryResult>(It.IsAny<GetQuestionsQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.GetByTestId(testId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().Count(), queryResult.Questions.Count());
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().First().Text, queryResult.Questions.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().First().Id, queryResult.Questions.First().Id);
        }

        [Test]
        public async Task GetByTestId_InvalidIdGiven_ReturnsNotFound()
        {
            var testId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionsQuery, GetQuestionsQueryResult>(It.IsAny<GetQuestionsQuery>()))
                .Returns(Task.FromResult(new GetQuestionsQueryResult()));

            var result = await _controller.GetByTestId(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = _fixture.Create<QuestionViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddQuestionCommand>(It.IsAny<AddQuestionCommand>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Post(viewModel) as OkResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var result = await _controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = _fixture.Create<QuestionViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateQuestionCommand>(It.IsAny<UpdateQuestionCommand>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Put(viewModel) as OkResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Put_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var result = await _controller.Put(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task Put_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var viewModel = _fixture.Create<QuestionViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateQuestionCommand>(It.IsAny<UpdateQuestionCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Put(viewModel) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveQuestionCommand>(It.IsAny<RemoveQuestionCommand>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveQuestionCommand>(It.IsAny<RemoveQuestionCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Delete(id) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
