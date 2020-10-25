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
    public class QuestionController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQuestionViewModelConverter _converter;
        private readonly IQuestionDtoConverter _dtoConverter;

        public QuestionController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IQuestionViewModelConverter converter, 
            IQuestionDtoConverter dtoConverter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _converter = converter;
            _dtoConverter = dtoConverter;
        }


        /// <summary>
        /// GET: api/question/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>QuestionViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetQuestionQuery, GetQuestionQueryResult>(new GetQuestionQuery
            {
                Id = id
            });

            var viewModel = _converter.Convert(queryResult.Question);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Question with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }

        /// <summary>
        /// GET: api/question
        /// </summary>
        /// <param name="testId"></param>
        /// <returns>All QuestionViewModel for given {testId}</returns>
        [HttpGet]
        public async Task<IActionResult> GetByTestId([Required][FromQuery(Name = "testId")] int testId)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetQuestionsQuery, GetQuestionsQueryResult>(new GetQuestionsQuery
            {
                TestId = testId
            });

            var viewModels = _converter.Convert(queryResult.Questions);

            if (viewModels == null || !viewModels.Any())
            {
                return NotFound(new
                {
                    Error = $"Questions for test with identifier {testId} were not found"
                });
            }

            return new JsonResult(viewModels, JsonSettings);
        }

        /// <summary>
        /// POST: api/question/post
        /// </summary>
        /// <param name="viewModel">QuestionViewModel with data</param>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody] QuestionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<AddQuestionCommand>(new AddQuestionCommand
                {
                    Question = _dtoConverter.Convert(viewModel)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }

        }

        /// <summary>
        /// PUT: api/question/put
        /// </summary>
        /// <param name="viewModel">QuestionViewModel with data</param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] QuestionViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<UpdateQuestionCommand>(new UpdateQuestionCommand
                {
                    Question = _dtoConverter.Convert(viewModel)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/question/delete
        /// </summary>
        /// <param name="id">Identifier of QuestionViewModel</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commandDispatcher.DispatchAsync<RemoveQuestionCommand>(new RemoveQuestionCommand
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
