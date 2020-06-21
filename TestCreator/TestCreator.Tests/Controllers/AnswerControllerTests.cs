using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Models.DTO;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Converters.ViewModel;
using TestCreator.WebApp.Data.Queries.Interfaces;
using TestCreator.WebApp.ViewModels;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class AnswerControllerTests
    {
        [Test]
        public async Task Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetAnswerQueryResult
            {
                Answer = new Answer
                {
                    Id = 1,
                    Text = "Text1"
                }
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetAnswerQuery, GetAnswerQueryResult>(It.IsAny<GetAnswerQuery>()))
                .Returns(Task.FromResult(queryResult));

            var controller = new AnswerController(mockQuery.Object, null, new AnswerViewModelConverter(), null);

            var result = await controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Text"), queryResult.Answer.Text);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), queryResult.Answer.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetAnswerQuery, GetAnswerQueryResult>(It.IsAny<GetAnswerQuery>()))
                .Returns(Task.FromResult(new GetAnswerQueryResult()));

            var controller = new AnswerController(mockQuery.Object, null, new AnswerViewModelConverter(), null);

            var result = await controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetByQuestionId_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetAnswersQueryResult
            {
                Answers = new List<Answer>
                {
                    new Answer
                    {
                        Id = 1,
                        Text = "Text1"
                    },
                    new Answer
                    {
                        Id = 2,
                        Text = "Text2"
                    }
                }
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetAnswersQuery, GetAnswersQueryResult>(It.IsAny<GetAnswersQuery>()))
                .Returns(Task.FromResult(queryResult));

            var controller = new AnswerController(mockQuery.Object, null, new AnswerViewModelConverter(), null);

            var result = await controller.GetByQuestionId(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().Count(), queryResult.Answers.Count());
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().First().Text, queryResult.Answers.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<AnswerViewModel>().First().Id, queryResult.Answers.First().Id);
        }

        [Test]
        public async Task GetByQuestionId_InvalidIdGiven_ReturnsNotFound()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetAnswersQuery, GetAnswersQueryResult>(It.IsAny<GetAnswersQuery>()))
                .Returns(Task.FromResult<GetAnswersQueryResult>(new GetAnswersQueryResult()));

            var controller = new AnswerController(mockQuery.Object, null, new AnswerViewModelConverter(), null);

            var result = await controller.GetByQuestionId(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        //[Test]
        //public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new AnswerViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IAnswerRepository>();
        //    mockRepo.Setup(x => x.CreateAnswer(It.IsAny<AnswerViewModel>())).Returns(viewModel);


        //    var controller = new AnswerController(mockRepo.Object);

        //    var result = controller.Post(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Text, viewModel.Text);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Id, viewModel.Id);
        //}

        //[Test]
        //public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<IAnswerRepository>();

        //    var controller = new AnswerController(mockRepo.Object);

        //    var result = controller.Post(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new AnswerViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IAnswerRepository>();
        //    mockRepo.Setup(x => x.UpdateAnswer(It.IsAny<AnswerViewModel>())).Returns(viewModel);

        //    var controller =
        //        new AnswerController(mockRepo.Object);

        //    var result = controller.Put(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Text, viewModel.Text);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<AnswerViewModel>().Id, viewModel.Id);
        //}

        //[Test]
        //public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<IAnswerRepository>();

        //    var controller = new AnswerController(mockRepo.Object);

        //    var result = controller.Put(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Put_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var viewModel = new AnswerViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IAnswerRepository>();
        //    mockRepo.Setup(x => x.UpdateAnswer(It.IsAny<AnswerViewModel>())).Returns<AnswerViewModel>(null);

        //    var controller = new AnswerController(mockRepo.Object);

        //    var result = controller.Put(viewModel);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    int id = 1;

        //    var mockRepo = new Mock<IAnswerRepository>();
        //    mockRepo.Setup(x => x.DeleteAnswer(It.IsAny<int>())).Returns(true);

        //    var controller =
        //        new AnswerController(mockRepo.Object);

        //    var result = controller.Delete(id);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NoContentResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var mockRepo = new Mock<IAnswerRepository>();
        //    mockRepo.Setup(x => x.DeleteAnswer(1)).Returns(false);

        //    var controller = new AnswerController(mockRepo.Object);

        //    var result = controller.Delete(2);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}
    }
}
