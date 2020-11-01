using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Commands;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    public class TokenController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public TokenController(ITokenService tokenService, IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _tokenService = tokenService;
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody] TokenRequestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            switch (viewModel.GrantType)
            {
                case "password":
                    return await GetToken(viewModel);
                case "refresh_token":
                    return await RefreshToken(viewModel);
                default:
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> RefreshToken(TokenRequestViewModel viewModel)
        {
            try
            {
                var refreshTokenResult = await _queryDispatcher.DispatchAsync<GetTokenQuery, GetTokenQueryResult>(new GetTokenQuery
                {
                    ClientId = viewModel.ClientId,
                    RefreshToken = viewModel.RefreshToken
                });

                if (refreshTokenResult.Token == null)
                {
                    return new UnauthorizedResult();
                }

                var userResult = await _queryDispatcher.DispatchAsync<GetUserByIdQuery, GetUserQueryResult>(new GetUserByIdQuery
                {
                    UserId = refreshTokenResult.Token.UserId
                });

                if (userResult.User == null)
                {
                    return new UnauthorizedResult();
                }

                var newRefreshToken = _tokenService.GenerateRefreshToken(refreshTokenResult.Token.ClientId,
                    refreshTokenResult.Token.UserId);

                await _commandDispatcher.DispatchAsync<RemoveTokenCommand>(new RemoveTokenCommand
                {
                    Id = refreshTokenResult.Token.Id
                });
                await _commandDispatcher.DispatchAsync<AddTokenCommand>(new AddTokenCommand
                {
                    Token = refreshTokenResult.Token
                });

                var tokenData = _tokenService.CreateAccessToken(newRefreshToken.UserId);

                var response = new TokenResponseViewModel
                {
                    Expiration = tokenData.ExporationTimeInMinutes,
                    RefreshToken = newRefreshToken.Value,
                    Token = tokenData.EncodedToken
                };

                return Json(response);

            }
            catch (Exception)
            {
                return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> GetToken(TokenRequestViewModel viewModel)
        {
            try
            {
                var userResult = await _queryDispatcher.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(new GetUserByNameQuery
                {
                    UserName = viewModel.Username
                });

                if (userResult.User == null && viewModel.Username.Contains("@"))
                {
                    userResult = await _queryDispatcher.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(new GetUserByEmailQuery
                    {
                        Email = viewModel.Username
                    }); 
                }

                var checkUserPasswordResult = await _queryDispatcher.DispatchAsync<CheckUserPasswordQuery, CheckUserPasswordQueryResult>(new CheckUserPasswordQuery
                {
                    User = userResult.User,
                    Password = viewModel.Password
                });

                if (userResult.User == null || !checkUserPasswordResult.IsCorrect)
                {
                    return new UnauthorizedResult();
                }

                var token = _tokenService.GenerateRefreshToken(viewModel.ClientId, userResult.User.Id);

                await _commandDispatcher.DispatchAsync<AddTokenCommand>(new AddTokenCommand
                {
                    Token = token
                });

                var accessTokenData = _tokenService.CreateAccessToken(userResult.User.Id);

                var response = new TokenResponseViewModel
                {
                    Token = accessTokenData.EncodedToken,
                    Expiration = accessTokenData.ExporationTimeInMinutes,
                    RefreshToken = token.Value
                };

                return Json(response);
            }
            catch (Exception)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
