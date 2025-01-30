using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AnimeInfo.Controllers;
using AnimeInfo.Models;
using AnimeInfo.Data;
using Moq;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AnimeInfo.Tests
{

    public class BlogControllerTests
    {
        private readonly Mock<IBlogRepository> _mockRepo;
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<ILogger<BlogController>> _mockLogger;

        public BlogControllerTests()
        {
            _mockRepo = new Mock<IBlogRepository>();

        
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            _mockUserManager = new Mock<UserManager<AppUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            _mockLogger = new Mock<ILogger<BlogController>>();
        }

        [Fact]
        public void Post_GET_ReturnsViewWithModel()
        {

            var controller = new BlogController(_mockRepo.Object, _mockUserManager.Object, _mockLogger.Object);
        

        
            var result = controller.Post() as ViewResult;
            var model = result?.Model as Blog;

         
            Assert.NotNull(result);
            Assert.NotNull(model);
            
        }

        [Fact]
        public async Task Post_POST_ReturnsRedirectToIndex()
        {

            var controller = new BlogController(_mockRepo.Object, _mockUserManager.Object, _mockLogger.Object);
            var blog = new Blog
            {
                BlogTitle = "Test Blog",
                BlogText = "Test Content"
            };

            var testUser = new AppUser { Id = "1", UserName = "test@test.com" };
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(testUser);

            var result = await controller.Post(blog) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
