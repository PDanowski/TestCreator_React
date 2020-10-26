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
    public class AnswerController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IAnswerViewModelConverter _viewModelConverter;
        private readonly IAnswerDtoConverter _dtoConverter;

        public AnswerController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IAnswerViewModelConverter viewModelConverter, 
            IAnswerDtoConverter dtoConverter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _viewModelConverter = viewModelConverter;
            _dtoConverter = dtoConverter;
        }

        /// <summary>
        /// GET: api/answer/
        /// </summary>
        /// <returns>All AnswerViewModel for given {questionId}</returns>
        [HttpGet]
        public async Task<IActionResult> GetByQuestionId([Required][FromQuery(Name = "questionId")] int questionId)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetAnswersQuery, GetAnswersQueryResult>(new GetAnswersQuery
            {
                QuestionId = questionId
            });

            var viewModels = _viewModelConverter.Convert(queryResult.Answers);

            if (viewModels == null || !viewModels.Any())
            {
                return NotFound(new
                {
                    Error = $"Answers for question with identifier {questionId} were not found"
                });
            }

            return new JsonResult(viewModels, JsonSettings);
        }

        /// <summary>
        /// GET: api/answer/get/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AnswerViewModel with given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var queryResult = await _queryDispatcher.DispatchAsync<GetAnswerQuery, GetAnswerQueryResult>(new GetAnswerQuery
            {
                Id = id
            });

            var viewModel = _viewModelConverter.Convert(queryResult.Answer);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Answer with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }

        /// <summary>
        /// POST: api/answer/post
        /// </summary>
        /// <param name="viewModel">AnswerViewModel with data</param>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AnswerViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<AddAnswerCommand>(new AddAnswerCommand
                {
                    Answer = _dtoConverter.Convert(viewModel)
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
        public async Task<IActionResult> Put([FromBody] AnswerViewModel viewModel)
        {
            if (viewModel == null)
            {
                return new StatusCodeResult(500);
            }

            try
            {
                await _commandDispatcher.DispatchAsync<UpdateAnswerCommand>(new UpdateAnswerCommand
                {
                    Answer = _dtoConverter.Convert(viewModel)
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
                await _commandDispatcher.DispatchAsync<RemoveAnswerCommand>(new RemoveAnswerCommand
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
