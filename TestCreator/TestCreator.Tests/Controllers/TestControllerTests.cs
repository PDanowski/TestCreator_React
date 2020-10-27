using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Http;
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
    public class TestControllerTests
    {
        private TestController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _controller = new TestController(_queryDispatcherMock.Object, _commandDispatcherMock.Object, new TestViewModelConverter(), new TestDtoConverter());
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
            var queryResult = _fixture.Create<GetTestQueryResult>();
            var testId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetTestQuery, GetTestQueryResult>(It.IsAny<GetTestQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.Get(testId) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), queryResult.Test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), queryResult.Test.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            var testId = _fixture.Create<int>();

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetTestQuery, GetTestQueryResult>(It.IsAny<GetTestQuery>()))
                .Returns(Task.FromResult<GetTestQueryResult>(new GetTestQueryResult()));

            var result = await _controller.Get(testId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Put_CorrectViewModelGiven_ReturnsOkResult()
        {
            var viewModel = _fixture.Create<TestViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<UpdateTestCommand>(It.IsAny<UpdateTestCommand>()))
                .Returns(Task.CompletedTask);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "name1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext {User = user}
            };

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
        public async Task Post_CorrectViewModelGiven_ReturnsOkResult()
        {
            var viewModel = _fixture.Create<TestViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddTestCommand>(It.IsAny<AddTestCommand>()))
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
        public async Task Post_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsObjectResult()
        {
            var viewModel = _fixture.Create<TestViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddTestCommand>(It.IsAny<AddTestCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Post(viewModel) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task Delete_CorrectViewModelGiven_ReturnsNoContentResult()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveTestCommand>(It.IsAny<RemoveTestCommand>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Delete_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        {
            var id = _fixture.Create<int>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveTestCommand>(It.IsAny<RemoveTestCommand>()))
                .Returns(Task.FromException(new InvalidOperationException()));

            var result = await _controller.Delete(id) as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
