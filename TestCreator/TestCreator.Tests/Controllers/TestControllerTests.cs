using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestCreator.Data.Queries;
using TestCreator.Data.Queries.Results;
using TestCreator.Tests.Helpers;
using TestCreator.WebApp.Controllers;
using TestCreator.WebApp.Converters.ViewModel;
using TestCreator.WebApp.Data.Queries.Interfaces;

namespace TestCreator.Tests.Controllers
{
    [TestFixture]
    public class TestControllerTests
    {
        [Test]
        public async Task Get_CorrectIdGiven_ReturnsJsonViewModel()
        {
            var queryResult = new GetTestQueryResult()
            {
                Test = new Data.Models.DTO.Test
                {
                    Id = 1,
                    Title = "title1"
                }
            };
            
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x => x.DispatchAsync<GetTestQuery, GetTestQueryResult>(It.IsAny<GetTestQuery>()))
                .Returns(Task.FromResult(queryResult));

            var controller = new TestController(mockQuery.Object, null, new TestViewModelConverter());

            var result = await controller.Get(1) as JsonResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), queryResult.Test.Title);
            Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), queryResult.Test.Id);
        }

        [Test]
        public async Task Get_InvalidIdGiven_ReturnsNotFound()
        {
            var mockQuery = new Mock<IQueryDispatcher>();
            mockQuery.Setup(x => x.DispatchAsync<GetTestQuery, GetTestQueryResult>(It.IsAny<GetTestQuery>()))
                .Returns(Task.FromResult<GetTestQueryResult>(null));

            var controller = new TestController(mockQuery.Object, null, new TestViewModelConverter());

            var result = await controller.Get(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        //[Test]
        //public void Put_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new TestViewModel
        //    {
        //        Id = 1,
        //        Title = "title1"
        //    };

        //    var mockRepo = new Mock<ITestRepository>();
        //    mockRepo.Setup(x => x.CreateTest(It.IsAny<TestViewModel>())).Returns(viewModel);

        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, "name1")
        //    }, "mock"));

        //    var controller = new TestController(mockRepo.Object, null)
        //    {
        //        ControllerContext = new ControllerContext
        //        {
        //            HttpContext = new DefaultHttpContext { User = user }
        //        }
        //    };

        //    var result = controller.Put(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), viewModel.Title);
        //    Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), viewModel.Id);
        //}

        //[Test]
        //public void Put_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<ITestRepository>();

        //    var controller = new TestController(mockRepo.Object, null);

        //    var result = controller.Put(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Post_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    var viewModel = new TestViewModel
        //    {
        //        Id = 1,
        //        Title = "title1"
        //    };

        //    var mockRepo = new Mock<ITestRepository>();
        //    mockRepo.Setup(x => x.UpdateTest(It.IsAny<TestViewModel>())).Returns(viewModel);

        //    var controller =
        //        new TestController(mockRepo.Object, null);

        //    var result = controller.Post(viewModel) as JsonResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.GetValueFromJsonResult<string>("Title"), viewModel.Title);
        //    Assert.AreEqual(result.GetValueFromJsonResult<int>("Id"), viewModel.Id);
        //}

        //[Test]
        //public void Post_InvalidViewModelGiven_ReturnsStatusCode500()
        //{
        //    var mockRepo = new Mock<ITestRepository>();

        //    var controller = new TestController(mockRepo.Object, null);

        //    var result = controller.Post(null) as StatusCodeResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(result.StatusCode, 500);
        //}

        //[Test]
        //public void Post_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var viewModel = new TestViewModel
        //    {
        //        Id = 1,
        //        Title = "title1"
        //    };

        //    var mockRepo = new Mock<ITestRepository>();
        //    mockRepo.Setup(x => x.UpdateTest(It.IsAny<TestViewModel>())).Returns<TestViewModel>(null);

        //    var controller = new TestController(mockRepo.Object, null);

        //    var result = controller.Post(viewModel);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectViewModelGiven_ReturnsJsonViewModel()
        //{
        //    int id = 1;

        //    var mockRepo = new Mock<ITestRepository>();
        //    mockRepo.Setup(x => x.DeleteTest(It.IsAny<int>())).Returns(true);

        //    var controller =
        //        new TestController(mockRepo.Object, null);

        //    var result = controller.Delete(id);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NoContentResult>(result);
        //}

        //[Test]
        //public void Delete_CorrectTestAttemptViewModelErrorDuringProcessing_ReturnsNotFound()
        //{
        //    var mockRepo = new Mock<ITestRepository>();
        //    mockRepo.Setup(x => x.DeleteTest(1)).Returns(false);

        //    var controller = new TestController(mockRepo.Object, null);

        //    var result = controller.Delete(2);

        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}
    }
}
