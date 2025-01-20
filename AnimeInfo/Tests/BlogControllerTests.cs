using Microsoft.AspNetCore.Mvc;

using AnimeInfo.Controllers;
using AnimeInfo.Models;
using AnimeInfo.Data;
using Moq;

namespace AnimeInfo.Tests
{
    public class BlogControllerTests
    {
        [Fact]
        public void Post_GET_ReturnsViewWithModel()
        {
           
            var mockRepo = new Mock<IBlogRepository>();
            var controller = new BlogController(mockRepo.Object);

        
            var result = controller.Post() as ViewResult;
            var model = result?.Model as Blog;

         
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.NotNull(model.BlogAuthor);
        }

        [Fact]
        public void Post_POST_ReturnsRedirectToIndex()
        {
          
            var mockRepo = new Mock<IBlogRepository>();
            var controller = new BlogController(mockRepo.Object);
            var blog = new Blog
            {
                BlogTitle = "Test Blog",
                BlogText = "Test Content",
                BlogAuthor = new AppUser()
            };

         
            var result = controller.Post(blog) as RedirectToActionResult;

         
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}