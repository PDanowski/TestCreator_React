using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Commands;
using TestCreator.Data.Consts;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IApplicationUserDtoConverter _converter;

        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher, IApplicationUserDtoConverter converter)
        {
            _commandDispatcher = commandDispatcher;
            _converter = converter;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// POST: api/user/put
        /// </summary>
        /// <param name="viewModel">UserViewModel with data</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            var result = await _queryDispatcher.DispatchAsync<GetUserByNameQuery, GetUserQueryResult>(new GetUserByNameQuery
            {
                UserName = viewModel.UserName
            });

            if (result.User != null)
            {
                return BadRequest("User with given username already exists");
            }

            result = await _queryDispatcher.DispatchAsync<GetUserByEmailQuery, GetUserQueryResult>(new GetUserByEmailQuery
            {
                Email = viewModel.Email
            });

            if (result.User != null)
            {
                return BadRequest("User with given e-mail already exists");
            }

            try
            {
                await _commandDispatcher.DispatchAsync<AddUserCommand>(new AddUserCommand
                {
                    User = _converter.Convert(viewModel),
                    Roles = new[] { UserRoles.RegisteredUser }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }

            return Ok();
        }
    }
}
