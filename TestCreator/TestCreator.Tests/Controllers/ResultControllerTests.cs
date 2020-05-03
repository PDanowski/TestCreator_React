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
    public class ResultControllerTests
    {
        [Test]
        public async Task Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetResultQueryResult
            {
                Result = new Result
                {
                    Id = 1,
                    Text = "Text1"
                }
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetResultQuery, GetResultQueryResult>(It.IsAny<GetResultQuery>()))
                .Returns(Task.FromResult(queryResult));

            var controller = new ResultController(mockQuery.Object, null, new ResultViewModelConverter());

            var result = await controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, queryResult.Result.Text);
            Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, queryResult.Result.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetResultQuery, GetResultQueryResult>(It.IsAny<GetResultQuery>()))
                .Returns(Task.FromResult<GetResultQueryResult>(new GetResultQueryResult()));

            var controller = new ResultController(mockQuery.Object, null, new ResultViewModelConverter());

            var result = await controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetByTestId_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetResultsQueryResult
            {
                Results = new List<Result>
                {
                    new Result
                    {
                        Id = 1,
                        Text = "Text1"
                    },
                    new Result
                    {
                        Id = 2,
                        Text = "Text2"
                    }
                }
            };

            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetResultsQuery, GetResultsQueryResult>(It.IsAny<GetResultsQuery>()))
                .Returns(Task.FromResult(queryResult));

            var controller = new ResultController(mockQuery.Object, null, new ResultViewModelConverter());

            var result = await controller.GetByTestId(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().Count(), queryResult.Results.Count());
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().First().Text, queryResult.Results.First().Text);
            Assert.AreEqual(result.GetIEnumberableFromJsonResult<ResultViewModel>().First().Id, queryResult.Results.First().Id);
        }

        [Test]
        public async Task GetByTestId_InvalidIdGiven_ReturnsNotFound()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x =>
                    x.DispatchAsync<GetResultsQuery, GetResultsQueryResult>(It.IsAny<GetResultsQuery>()))
                .Returns(Task.FromResult(new GetResultsQueryResult()));

            var controller = new ResultController(mockQuery.Object, null, new ResultViewModelConverter());

            var result = await controller.GetByTestId(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        //[Test]
        //public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new ResultViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IResultRepository>();
        //    mockRepo.Setup(x => x.CreateResult(It.IsAny<ResultViewModel>())).Returns(viewModel);


        //    var controller = new ResultController(mockRepo.Object);

        //    var result = controller.Post(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, viewModel.Text);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, viewModel.Id);
        //}

        //[Test]
        //public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<IResultRepository>();

        //    var controller = new ResultController(mockRepo.Object);

        //    var result = controller.Post(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new ResultViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IResultRepository>();
        //    mockRepo.Setup(x => x.UpdateResult(It.IsAny<ResultViewModel>())).Returns(viewModel);

        //    var controller =
        //        new ResultController(mockRepo.Object);

        //    var result = controller.Put(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Text, viewModel.Text);
        //    Assert.AreEqual(result.GetObjectFromJsonResult<ResultViewModel>().Id, viewModel.Id);
        //}

        //[Test]
        //public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<IResultRepository>();

        //    var controller = new ResultController(mockRepo.Object);

        //    var result = controller.Put(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Put_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var viewModel = new ResultViewModel
        //    {
        //        Id = 1,
        //        Text = "Text1"
        //    };

        //    var mockRepo = new Mock<IResultRepository>();
        //    mockRepo.Setup(x => x.UpdateResult(It.IsAny<ResultViewModel>())).Returns<ResultViewModel>(null);

        //    var controller = new ResultController(mockRepo.Object);

        //    var result = controller.Put(viewModel);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    int id = 1;

        //    var mockRepo = new Mock<IResultRepository>();
        //    mockRepo.Setup(x => x.DeleteResult(It.IsAny<int>())).Returns(true);

        //    var controller =
        //        new ResultController(mockRepo.Object);

        //    var result = controller.Delete(id);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NoContentResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var mockRepo = new Mock<IResultRepository>();
        //    mockRepo.Setup(x => x.DeleteResult(1)).Returns(false);

        //    var controller = new ResultController(mockRepo.Object);

        //    var result = controller.Delete(2);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}
    }
}
