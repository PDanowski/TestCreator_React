using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Commands;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    public class ResultController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IResultViewModelConverter _converter;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IResultDtoConverter _dtoConverter;


        public ResultController(IQueryDispatcher queryDispatcher, 
            IResultViewModelConverter converter, 
            ICommandDispatcher commandDispatcher, 
            IResultDtoConverter dtoConverter)
        {
            _queryDispatcher = queryDispatcher;
            _converter = converter;
            _commandDispatcher = commandDispatcher;
            _dtoConverter = dtoConverter;
        }

        /// <summary>
        /// GET: api/result/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ResultViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetResultQuery, GetResultQueryResult>(new GetResultQuery
            {
                Id = id
            });

            var viewModel = _converter.Convert(queryResult.Result);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Result with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }

        /// <summary>
        /// GET: api/result
        /// </summary>
        /// <param name="testId"></param>
        /// <returns>All ResultViewModel for given {testId}</returns>
        [HttpGet]
        public async Task<IActionResult> GetByTestId([Required][FromQuery(Name = "testId")] int testId)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetResultsQuery, GetResultsQueryResult>(new GetResultsQuery
            {
                TestId = testId
            });

            var viewModels = _converter.Convert(queryResult.Results);

            if (viewModels == null || !viewModels.Any())
            {
                return NotFound(new
                {
                    Error = $"Results for test with identifier {testId} were not found"
                });
            }

            return new JsonResult(viewModels, JsonSettings);
        }

        /// <summary>
        /// POST: api/answer/post
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ResultViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<AddResultCommand>(new AddResultCommand
                {
                    Result = _dtoConverter.Convert(viewModel)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }

        }

        /// <summary>
        /// PUT: api/answer/put
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] ResultViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<UpdateResultCommand>(new UpdateResultCommand
                {
                    Result = _dtoConverter.Convert(viewModel)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/answer/delete
        /// </summary>
        /// <param name="id">Identifier of AnswerViewModel</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commandDispatcher.DispatchAsync<RemoveResultCommand>(new RemoveResultCommand
                {
                    Id = id
                });
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }
    }
}
