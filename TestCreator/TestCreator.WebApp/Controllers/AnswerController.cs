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
    public class AnswerController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IAnswerViewModelConverter _converter;

        public AnswerController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IAnswerViewModelConverter converter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _converter = converter;
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

            var viewModels = _converter.Convert(queryResult.Answers);

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

            var viewModel = _converter.Convert(queryResult.Answer);

            if (viewModel == null)
            {
                return NotFound(new
                {
                    Error = $"Answer with identifier {id} was not found"
                });
            }

            return new JsonResult(viewModel, JsonSettings);
        }
    }
}
