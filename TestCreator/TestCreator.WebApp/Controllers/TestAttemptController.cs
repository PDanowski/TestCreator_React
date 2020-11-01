using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.WebApp.Controllers.Base;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.Mappers.Interfaces;
using TestCreator.WebApp.Services.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.WebApp.Controllers
{
    [ApiController]
    public class TestAttemptController : BaseApiController
    {
        private readonly ITestAttemptViewModelMapper _mapper;
        private readonly ITestResultCalculationService _service;
        private readonly IQueryDispatcher _queryDispatcher;

        public TestAttemptController(ITestAttemptViewModelMapper mapper, 
            ITestResultCalculationService service, 
            IQueryDispatcher queryDispatcher)
        {
            _mapper = mapper;
            _service = service;
            _queryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// GET: api/testAttempt/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single TestAttemptViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetTestAttemptQuery, GetTestAttemptQueryResult>(new GetTestAttemptQuery
            {
                Id = id
            });

            var viewModel = _mapper.Convert(queryResult);

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
        /// POST: api/testAttempt
        /// </summary>
        /// <param name="viewModel">TestAttemptViewModel with data</param>
        /// <returns>Calculate result and return TestAttemptResultViewModel for given {viewModel}</returns>
        [HttpPost]
        public IActionResult CalculateResult(TestAttemptViewModel viewModel)
        {
            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Error - argument is incorrect"
                });
            }

            var resultViewModel = _service.CalculateResult(viewModel);

            if (resultViewModel == null)
            {
                return new StatusCodeResult(500);
            }

            return new JsonResult(resultViewModel, JsonSettings);
        }
    }
}
