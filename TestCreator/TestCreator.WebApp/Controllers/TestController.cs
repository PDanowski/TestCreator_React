using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Data.Commands.Interfaces;

namespace TestCreator.WebApp.Controllers
{
    [ApiController]
    public class TestController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public TestController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// GET: api/test/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single TestViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var viewModel = await _queryDispatcher.DispatchAsync<GetTestQuery, GetTestQueryResult>(new GetTestQuery
            {
                Id = id
            });

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Test with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }
    }
}
