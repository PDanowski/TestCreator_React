using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Commands;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Consts;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Attributes;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Converters.DTO.Interfaces;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    [ApiController]
    public class TestController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ITestViewModelConverter _converter;
        private readonly ITestDtoConverter _dtoConverter;

        private int _defaultQuerySize = 10;


        public TestController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            ITestViewModelConverter converter, 
            ITestDtoConverter dtoConverter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _converter = converter;
            _dtoConverter = dtoConverter;
        }

        /// <summary>
        /// GET: api/test/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single TestViewModel with given {id}</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetTestQuery, GetTestQueryResult>(new GetTestQuery
            {
                Id = id
            });

            var viewModel = _converter.Convert(queryResult.Test);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Test with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }


        /// <summary>
        /// GET api/test
        /// </summary>
        /// <param name="size"></param>
        /// <param name="sorting">0 - random, 1 - latest, 2 - by title</param>
        /// <returns>{num} TestViewModel, sorted by param: {sorting}</returns>
        [HttpGet]
        public async Task<IActionResult> GetBySorting([FromQuery]int sorting, [FromQuery]int? size = 10)
        {
            TestsOrder order;

            switch (sorting)
            {
                case 0:
                    order = TestsOrder.Random;
                    break;
                case 1:
                    order = TestsOrder.Latest;
                    break;
                case 2:
                    order = TestsOrder.ByTitle;
                    break;
                default:
                    return NotFound(new
                    {
                        Error = $"Sorting parameter has wrong value: {sorting}"
                    });
            }

            GetTestsQueryResult queryResult =
                await _queryDispatcher.DispatchAsync<GetTestsByParamQuery, GetTestsQueryResult>(new GetTestsByParamQuery
                {
                    Param = order,
                    Number = size ?? _defaultQuerySize
                });


            return new JsonResult(_converter.Convert(queryResult.Tests), JsonSettings);
        }


        /// <summary>
        /// GET: api/test
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="size"></param>
        /// <returns>{num} TestViewModels searched by keyword</returns>
        [HttpGet]
        [ExactQueryParam("keyword")]
        public async Task<IActionResult> GetByKeyword([FromQuery]string keyword)
        {
            GetTestsQueryResult queryResult =
                await _queryDispatcher.DispatchAsync<GetTestsByKeywordQuery, GetTestsQueryResult>(new GetTestsByKeywordQuery
                {
                    Keyword = keyword,
                    Number = _defaultQuerySize
                });

            return new JsonResult(_converter.Convert(queryResult.Tests), JsonSettings);
        }

        /// <summary>
        /// PUT: api/test/put
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<UpdateTestCommand>(new UpdateTestCommand
                {
                    Test = _dtoConverter.Convert(viewModel)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// POST: api/test/post
        /// </summary>
        /// <param name="viewModel">TestViewModel with data</param>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<AddTestCommand>(new AddTestCommand
                {
                    Test = _dtoConverter.Convert(viewModel)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/test/delete
        /// </summary>
        /// <param name="id">Identifier of TestViewModel</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commandDispatcher.DispatchAsync<RemoveTestCommand>(new RemoveTestCommand
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
