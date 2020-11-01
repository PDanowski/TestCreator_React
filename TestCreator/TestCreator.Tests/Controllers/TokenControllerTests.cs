using System;
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
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.Services.ValueObjects;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TokenControllerTests
    {
        private TokenController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private Mock<ITokenService> _serviceMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _serviceMock = _fixture.Freeze<Mock<ITokenService>>();
            _controller = new TokenController(_serviceMock.Object, _queryDispatcherMock.Object, _commandDispatcherMock.Object);
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
            _commandDispatcherMock.Reset();
        }

        [Test]
        public void Auth_WhenBodyViewModelWithPasswordGrantTypeAndUserName_ShouldReturnTokenResponse()
        {
            //Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var token = _fixture.Create<Token>();

            var tokenData = new TokenData
            {
                ExporationTimeInMinutes = 60,
                EncodedToken = token.Value
            };

            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = userName,
                ClientId = token.ClientId,
                Password = password,
                GrantType = "password"
            };
            var userResult = new GetUserQueryResult
            {
                User = new ApplicationUser
                {
                    Id = token.UserId,
                    UserName = userName
                }
            };

            _serviceMock.Setup(x => x.GenerateRefreshToken(token.ClientId, token.UserId)).Returns(token);
            _serviceMock.Setup(x => x.CreateAccessToken(token.UserId)).Returns(tokenData);

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(userResult));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<CheckUserPasswordQuery, CheckUserPasswordQueryResult>(It.IsAny<CheckUserPasswordQuery>()))
                .Returns(Task.FromResult(new CheckUserPasswordQueryResult
                {
                    IsCorrect = true
                }));

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddTokenCommand>(It.IsAny<AddTokenCommand>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = _controller.Auth(viewModel).Result as JsonResult;

            //Assert
            Assert.IsNotNull(result);

            var json = new JsonResult(new TokenResponseViewModel
            {
                Expiration = tokenData.ExporationTimeInMinutes,
                RefreshToken = tokenData.EncodedToken,
                Token = token.Value
            });

            Assert.AreEqual(result.Value.ToString(), json.Value.ToString());
        }


        [Test]
        public void Auth_WhenBodyViewModelWithPasswordGrantTypeAndEmail_ShouldReturnTokenResponse()
        {
            //Arrange
            var userName = $"{_fixture.Create<string>()}@test.com";
            var password = _fixture.Create<string>();
            var token = _fixture.Create<Token>();

            var tokenData = new TokenData
            {
                ExporationTimeInMinutes = 60,
                EncodedToken = token.Value
            };

            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = userName,
                ClientId = token.ClientId,
                Password = password,
                GrantType = "password"
            };
            var userResult = new GetUserQueryResult
            {
                User = new ApplicationUser
                {
                    Id = token.UserId,
                    UserName = userName
                }
            };

            _serviceMock.Setup(x => x.GenerateRefreshToken(token.ClientId, token.UserId)).Returns(token);
            _serviceMock.Setup(x => x.CreateAccessToken(token.UserId)).Returns(tokenData);

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(It.IsAny<GetUserByEmailQuery>()))
                .Returns(Task.FromResult(userResult));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<CheckUserPasswordQuery, CheckUserPasswordQueryResult>(It.IsAny<CheckUserPasswordQuery>()))
                .Returns(Task.FromResult(new CheckUserPasswordQueryResult
                {
                    IsCorrect = true
                }));

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddTokenCommand>(It.IsAny<AddTokenCommand>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = _controller.Auth(viewModel).Result as JsonResult;

            //Assert
            Assert.IsNotNull(result);

            var json = new JsonResult(new TokenResponseViewModel
            {
                Expiration = tokenData.ExporationTimeInMinutes,
                RefreshToken = tokenData.EncodedToken,
                Token = token.Value
            });

            Assert.AreEqual(result.Value.ToString(), json.Value.ToString());
        }

        [Test]
        public void Auth_WhenBodyViewModelWithPasswordGrantTypeWrongPassword_ShouldReturnUnauthorizedResult()
        {
            //Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var token = _fixture.Create<Token>();

            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = userName,
                ClientId = token.ClientId,
                Password = password,
                GrantType = "password"
            };
            var userResult = new GetUserQueryResult
            {
                User = new ApplicationUser
                {
                    Id = token.UserId,
                    UserName = userName
                }
            };

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(It.IsAny<GetUserByNameQuery>()))
                .Returns(Task.FromResult(userResult));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<CheckUserPasswordQuery, CheckUserPasswordQueryResult>(It.IsAny<CheckUserPasswordQuery>()))
                .Returns(Task.FromResult(new CheckUserPasswordQueryResult
                {
                    IsCorrect = false
                }));

            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddTokenCommand>(It.IsAny<AddTokenCommand>()))
                .Verifiable();

            //Act
            var result = _controller.Auth(viewModel).Result;

            //Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void Auth_WhenNullBodyViewModel_ShouldReturnStatusCode500()
        {
            //Act
            var result = _controller.Auth(null).Result as StatusCodeResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public void Auth_WhenBodyViewModelWithRefreshTokenGrantTypeAndUserName_ShouldReturnTokenResponse()
        {
            //Arrange
            var userName = _fixture.Create<string>();
            var clientId = _fixture.Create<string>();
            var userId = _fixture.Create<string>();
            var password = _fixture.Create<string>();

            var refreshToken = new Token
            {
                ClientId = clientId,
                UserId = userId,
                Id = 1,
                CreationDate = DateTime.Now,
                Value = Guid.NewGuid().ToString()
            };

            var newRefreshToken = new Token
            {
                ClientId = clientId,
                UserId = userId,
                Id = 1,
                CreationDate = DateTime.Now,
                Value = Guid.NewGuid().ToString()
            };

            var tokenData = new TokenData
            {
                ExporationTimeInMinutes = 60,
                EncodedToken = refreshToken.Value
            };

            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = userName,
                ClientId = refreshToken.ClientId,
                Password = password,
                GrantType = "refresh_token"
            };

            var userResult = new GetUserQueryResult
            {
                User = new ApplicationUser
                {
                    Id = refreshToken.UserId,
                    UserName = userName
                }
            };

            var tokenResult = new GetTokenQueryResult
            {
                Token = refreshToken
            };

            _serviceMock.Setup(x => x.GenerateRefreshToken(refreshToken.ClientId, refreshToken.UserId)).Returns(newRefreshToken);
            _serviceMock.Setup(x => x.CreateAccessToken(refreshToken.UserId)).Returns(tokenData);

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByIdQuery, GetUserQueryResult>(It.IsAny<GetUserByIdQuery>()))
                .Returns(Task.FromResult(userResult));
            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetTokenQuery, GetTokenQueryResult>(It.IsAny<GetTokenQuery>()))
                .Returns(Task.FromResult(tokenResult));
            _commandDispatcherMock.Setup(x => x.DispatchAsync<AddTokenCommand>(It.IsAny<AddTokenCommand>()))
                .Returns(Task.CompletedTask);
            _commandDispatcherMock.Setup(x => x.DispatchAsync<RemoveTokenCommand>(It.IsAny<RemoveTokenCommand>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = _controller.Auth(viewModel).Result as JsonResult;

            //Assert
            Assert.IsNotNull(result);

            var json = new JsonResult(new TokenResponseViewModel
            {
                Expiration = tokenData.ExporationTimeInMinutes,
                RefreshToken = tokenData.EncodedToken,
                Token = newRefreshToken.Value
            });

            Assert.AreEqual(result.Value.ToString(), json.Value.ToString());
        }

        [Test]
        public void Auth_WhenBodyViewModelWithRefreshTokenGrantTypeAndWrongUserName_ShouldReturnUnauthorizedResult()
        {
            //Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var refreshToken = _fixture.Create<Token>();

            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = userName,
                ClientId = refreshToken.ClientId,
                Password = password,
                GrantType = "refresh_token"
            };

            _queryDispatcherMock.Setup(x => x.DispatchAsync<GetUserByIdQuery, GetUserQueryResult>(It.IsAny<GetUserByIdQuery>()))
                .Returns(Task.FromResult(new GetUserQueryResult()));

            //Act
            var result = _controller.Auth(viewModel).Result;

            //Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void Auth_WhenBodyViewModelWithRefreshTokenGrantTypeAndInvalidRefreshToken_ShouldReturnUnauthorizedResult()
        {
            //Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var clientId = Guid.NewGuid().ToString();

            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = userName,
                ClientId = clientId,
                Password = password,
                GrantType = "refresh_token"
            };

            _queryDispatcherMock.Setup(x => x.DispatchAsync<CheckUserPasswordQuery, CheckUserPasswordQueryResult>(It.IsAny<CheckUserPasswordQuery>()))
                .Returns(Task.FromResult(new CheckUserPasswordQueryResult
                {
                    IsCorrect = false
                }));

            //Act
            var result = _controller.Auth(viewModel).Result;

            //Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public void Auth_WhenInvalidViewModelWithRefreshTokenGrantType_ShouldReturnUnauthorizedResult()
        {
            //Arrange
            TokenRequestViewModel viewModel = new TokenRequestViewModel
            {
                Username = null,
                ClientId = null,
                Password = _fixture.Create<string>(),
                GrantType = "refresh_token"
            };

            _queryDispatcherMock.Setup(x => x.DispatchAsync<CheckUserPasswordQuery, CheckUserPasswordQueryResult>(It.IsAny<CheckUserPasswordQuery>()))
                .Returns(Task.FromResult(new CheckUserPasswordQueryResult
                {
                    IsCorrect = false
                }));

            //Act
            var result = _controller.Auth(viewModel).Result;

            //Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
    }
}
