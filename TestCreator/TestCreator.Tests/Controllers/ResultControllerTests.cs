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
    public class ResultControllerTests
    {
        private ResultController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _controller = new ResultController(
                _queryDispatcherMock.Object, new ResultViewModelConverter(), _commandDispatcherMock.Object, new ResultDtoConverter());
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
        }

        [Test]
        public async Task Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = _fixture.Create<GetResultQueryResult>();
            var resultId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetResultQuery, GetResultQueryResult>(It.IsAny<GetResultQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.Get(resultId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, queryResult.Result.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, queryResult.Result.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            var resultId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetResultQuery, GetResultQueryResult>(It.IsAny<GetResultQuery>()))
                .Returns(Task.FromResult<GetResultQueryResult>(new GetResultQueryResult()));

            var result = await _controller.Get(resultId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetByTestId_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = _fixture.Create<GetResultsQueryResult>();
            var testId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetResultsQuery, GetResultsQueryResult>(It.IsAny<GetResultsQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.GetByTestId(testId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().Count(), queryResult.Results.Count());
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().First().Text, queryResult.Results.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().First().Id, queryResult.Results.First().Id);
        }

        [Test]
        public async Task GetByTestId_InvalidIdGiven_ReturnsNotFound()
        {
            var testId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetResultsQuery, GetResultsQueryResult>(It.IsAny<GetResultsQuery>()))
                .Returns(Task.FromResult(new GetResultsQueryResult()));

            var result = await _controller.GetByTestId(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGiven_ReturnsOkResult()
        {
            var viewModel = _fixture.Create<ResultViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddResultCommand>(It.IsAny<AddResultCommand>()))
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
        public async Task Put_CorrectViewModelGiven_ReturnsOkResult()
        {
            var viewModel = _fixture.Create<ResultViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateResultCommand>(It.IsAny<UpdateResultCommand>()))
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
            var viewModel = _fixture.Create<ResultViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateResultCommand>(It.IsAny<UpdateResultCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Put(viewModel) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveResultCommand>(It.IsAny<RemoveResultCommand>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveResultCommand>(It.IsAny<RemoveResultCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Delete(id) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
