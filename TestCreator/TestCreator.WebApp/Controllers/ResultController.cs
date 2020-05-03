using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;

namespace TestCreator.WebApp.Controllers
{
    public class ResultController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IResultViewModelConverter _converter;

        public ResultController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IResultViewModelConverter converter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _converter = converter;
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
    }
}
