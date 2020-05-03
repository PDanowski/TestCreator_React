using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Consts;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Converters.ViewModel.Interfaces;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;

namespace TestCreator.WebApp.Controllers
{
    [ApiController]
    public class TestController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ITestViewModelConverter _converter;

        public TestController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            ITestViewModelConverter converter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _converter = converter;
        }

        /// <summary>
        /// GET: api/test/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single TestViewModel with given {id}</returns>
        [HttpGet("{id}")]
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
        /// <param name="num"></param>
        /// <param name="sorting">0 - random, 1 - latest, 2 - by title</param>
        /// <returns>{num} TestViewModel, sorted by param: {sorting}</returns>
        [HttpGet]
        public async Task<IActionResult> GetBySorting([FromQuery]int sorting = 0, [FromQuery]int num = 10)
        {
            TestsOrder order;

            switch (sorting)
            {
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
                    Number = num
                });


            return new JsonResult(_converter.Convert(queryResult.Tests), JsonSettings);
        }


        /// <summary>
        /// GET: api/test
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="num"></param>
        /// <returns>{num} TestViewModels searched by keyword</returns>
        [HttpGet]
        public async Task<IActionResult> GetByKeyword([FromQuery]string keyword, [FromQuery]int num = 10)
        {
            GetTestsQueryResult queryResult =
                await _queryDispatcher.DispatchAsync<GetTestsByKeywordQuery, GetTestsQueryResult>(new GetTestsByKeywordQuery
                {
                    Keyword = keyword,
                    Number = num
                });

            return new JsonResult(_converter.Convert(queryResult.Tests), JsonSettings);
        }
    }
}
