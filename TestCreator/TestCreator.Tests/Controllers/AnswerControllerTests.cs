using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Commands;
using TestCreator.Data.Models.DTO;
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
    public class AnswerControllerTests
    {
        private AnswerController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization()); 
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _controller = new AnswerController(_queryDispatcherMock.Object, _commandDispatcherMock.Object, new AnswerViewModelConverter(), new AnswerDtoConverter());
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
            var queryResult = _fixture.Create<GetAnswerQueryResult>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetAnswerQuery, GetAnswerQueryResult>(It.IsAny<GetAnswerQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.Get(queryResult.Answer.Id) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Text"), queryResult.Answer.Text);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), queryResult.Answer.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetAnswerQuery, GetAnswerQueryResult>(It.IsAny<GetAnswerQuery>()))
                .Returns(Task.FromResult(new GetAnswerQueryResult()));

            var result = await _controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetByQuestionId_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = _fixture.Create<GetAnswersQueryResult>();
            var questionId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetAnswersQuery, GetAnswersQueryResult>(It.IsAny<GetAnswersQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.GetByQuestionId(questionId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().Count(), queryResult.Answers.Count());
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().First().Text, queryResult.Answers.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().First().Id, queryResult.Answers.First().Id);
        }

        [Test]
        public async Task GetByQuestionId_InvalidIdGiven_ReturnsNotFound()
        {
            var questionId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetAnswersQuery, GetAnswersQueryResult>(It.IsAny<GetAnswersQuery>()))
                .Returns(Task.FromResult<GetAnswersQueryResult>(new GetAnswersQueryResult()));

            var result = await _controller.GetByQuestionId(questionId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var viewModel = _fixture.Create<AnswerViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddAnswerCommand>(It.IsAny<AddAnswerCommand>()))
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
            var viewModel = _fixture.Create<AnswerViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateAnswerCommand>(It.IsAny<UpdateAnswerCommand>()))
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
            var viewModel = _fixture.Create<AnswerViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateAnswerCommand>(It.IsAny<UpdateAnswerCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Put(viewModel) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveAnswerCommand>(It.IsAny<RemoveAnswerCommand>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveAnswerCommand>(It.IsAny<RemoveAnswerCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Delete(id) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
