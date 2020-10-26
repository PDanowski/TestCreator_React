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
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Converters.DTO;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _controller = new UserController(_commandDispatcherMock.Object, _queryDispatcherMock.Object, new ApplicationUserDtoConverter());
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
            _commandDispatcherMock.Reset();
        }

        [Test]
        public async Task Post_CorrectViewModelGivenUserDoesNotExist_ReturnsJsonViewModel()
        {
            var viewModel = _fixture.Create<UserViewModel>();

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddUserCommand>(It.IsAny<AddUserCommand>()))
                .Returns(Task.CompletedTask);
            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));

            var result = await _controller.Post(viewModel) as OkResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGivenUserWithNameExists_ReturnsJsonViewModel()
        {
            var viewModel = _fixture.Create<UserViewModel>();

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult
                {
                    User = new ApplicationUser()
                }));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));

            var result = await _controller.Post(viewModel) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGivenUserWithEmailExists_ReturnsJsonViewModel()
        {
            var viewModel = _fixture.Create<UserViewModel>();

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult
            {
                User = new ApplicationUser()
            }));

            var result = await _controller.Post(viewModel) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var result = await _controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
