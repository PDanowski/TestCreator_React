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
    public class QuestionController : BaseApiController
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQuestionViewModelConverter _converter;

        public QuestionController(IQueryDispatcher queryDispatcher, 
            ICommandDispatcher commandDispatcher, 
            IQuestionViewModelConverter converter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _converter = converter;
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
    }
}
