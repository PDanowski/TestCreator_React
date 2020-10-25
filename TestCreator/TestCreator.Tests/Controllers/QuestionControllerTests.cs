using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Converters.DTO;
using TestCreator.WebApp.Converters.ViewModel;
using TestCreator.WebApp.Data.Commands.Interfaces;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class QuestionControllerTests
    {
        private QuestionController _controller;
        private Mock<IQueryDispatcher> _queryDispatcherMock;
        private Mock<ICommandDispatcher> _commandDispatcherMock;
        private IFixture _fixture;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization()); 
            _queryDispatcherMock = _fixture.Freeze<Mock<IQueryDispatcher>>();
            _commandDispatcherMock = _fixture.Freeze<Mock<ICommandDispatcher>>();
            _controller = new QuestionController(_queryDispatcherMock.Object, _commandDispatcherMock.Object, new QuestionViewModelConverter(), new QuestionDtoConverter());
        }

        [TearDown]
        public void Reset()
        {
            _queryDispatcherMock.Reset();
            _commandDispatcherMock.Reset();
        }

        [Test]
        public async Task Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetQuestionQueryResult
            {
                Question = new Question
                {
                    Id = 1,
                    Text = "Text1"
                }
            };

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionQuery, GetQuestionQueryResult>(It.IsAny<GetQuestionQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, queryResult.Question.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, queryResult.Question.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionQuery, GetQuestionQueryResult>(It.IsAny<GetQuestionQuery>()))
                .Returns(Task.FromResult(new GetQuestionQueryResult()));

            var result = await _controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetByTestId_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetQuestionsQueryResult
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = 1,
                        Text = "Text1"
                    },
                    new Question
                    {
                        Id = 2,
                        Text = "Text2"
                    }
                }
            };

            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionsQuery, GetQuestionsQueryResult>(It.IsAny<GetQuestionsQuery>()))
                .Returns(Task.FromResult(queryResult));

            var result = await _controller.GetByTestId(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().Count(), queryResult.Questions.Count());
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().First().Text, queryResult.Questions.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<QuestionViewModel>().First().Id, queryResult.Questions.First().Id);
        }

        [Test]
        public async Task GetByTestId_InvalidIdGiven_ReturnsNotFound()
        {
            _queryDispatcherMock.Setup(x =>
                    x.DispatchAsync<GetQuestionsQuery, GetQuestionsQueryResult>(It.IsAny<GetQuestionsQuery>()))
                .Returns(Task.FromResult(new GetQuestionsQueryResult()));

            var result = await _controller.GetByTestId(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        //[Test]
        //public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new QuestionViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IQuestionRepository>();
        //    mockRepo.Setup(x => x.CreateQuestion(It.IsAny<QuestionViewModel>())).Returns(viewModel);


        //    var controller = new QuestionController(mockRepo.Object);

        //    var result = controller.Post(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, viewModel.Text);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, viewModel.Id);
        //}

        //[Test]
        //public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<IQuestionRepository>();

        //    var controller = new QuestionController(mockRepo.Object);

        //    var result = controller.Post(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new QuestionViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IQuestionRepository>();
        //    mockRepo.Setup(x => x.UpdateQuestion(It.IsAny<QuestionViewModel>())).Returns(viewModel);

        //    var controller =
        //        new QuestionController(mockRepo.Object);

        //    var result = controller.Put(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Text, viewModel.Text);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<QuestionViewModel>().Id, viewModel.Id);
        //}

        //[Test]
        //public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<IQuestionRepository>();

        //    var controller = new QuestionController(mockRepo.Object);

        //    var result = controller.Put(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Put_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var viewModel = new QuestionViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IQuestionRepository>();
        //    mockRepo.Setup(x => x.UpdateQuestion(It.IsAny<QuestionViewModel>())).Returns<QuestionViewModel>(null);

        //    var controller = new QuestionController(mockRepo.Object);

        //    var result = controller.Put(viewModel);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    int id = 1;

        //    var mockRepo = new Mock<IQuestionRepository>();
        //    mockRepo.Setup(x => x.DeleteQuestion(It.IsAny<int>())).Returns(true);

        //    var controller =
        //        new QuestionController(mockRepo.Object);

        //    var result = controller.Delete(id);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NoContentResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var mockRepo = new Mock<IQuestionRepository>();
        //    mockRepo.Setup(x => x.DeleteQuestion(1)).Returns(false);

        //    var controller = new QuestionController(mockRepo.Object);

        //    var result = controller.Delete(2);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}
    }
}
