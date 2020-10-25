using System.Threading.Tasks;
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
        [Test]
        public async Task Post_CorrectViewModelGivenUserDoesntExist_ReturnsJsonViewModel()
        {
            var viewModel = new UserViewModel
            {
                Email = "user1@wp.pl",
                UserName = "user1",
                Password = "password123"
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            var mockCommand = new Mock<ICommandDispatcher>();

            mockCommand.Setup(x => x.DispatchAsync<AddUserCommand>(It.IsAny<AddUserCommand>()))
                .Returns(Task.CompletedTask);
            mockQuery.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));
            mockQuery.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));

            var controller = new UserController(mockCommand.Object, mockQuery.Object, new ApplicationUserDtoConverter());

            var result = await controller.Post(viewModel) as OkResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGivenUserWithNameExists_ReturnsJsonViewModel()
        {
            var viewModel = new UserViewModel
            {
                Email = "user1@wp.pl",
                UserName = "user1",
                Password = "password123"
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            var mockCommand = new Mock<ICommandDispatcher>();

            mockQuery.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult
                {
                    User = new ApplicationUser()
                }));
            mockQuery.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));

            var controller = new UserController(mockCommand.Object, mockQuery.Object, new ApplicationUserDtoConverter());

            var result = await controller.Post(viewModel) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_CorrectViewModelGivenUserWithEmailExists_ReturnsJsonViewModel()
        {
            var viewModel = new UserViewModel
            {
                Email = "user1@wp.pl",
                UserName = "user1",
                Password = "password123"
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            var mockCommand = new Mock<ICommandDispatcher>();

            mockQuery.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));
            mockQuery.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult
            {
                User = new ApplicationUser()
            }));

            var controller = new UserController(mockCommand.Object, mockQuery.Object, new ApplicationUserDtoConverter());

            var result = await controller.Post(viewModel) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Post_InvalidViewModelGiven_ReturnsStatusCode500()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            var mockCommand = new Mock<ICommandDispatcher>();

            var controller = new UserController(mockCommand.Object, mockQuery.Object, new ApplicationUserDtoConverter());

            var result = await controller.Post(null) as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
