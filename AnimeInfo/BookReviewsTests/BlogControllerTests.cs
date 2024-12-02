using AnimeInfo.Controllers;
using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AnimeInfo.Tests
{
    public class BlogControllerTests
    {
        private readonly IBlogRepository _repository;
        private readonly BlogController _controller;

        public BlogControllerTests()
        {
            // Setup
            _repository = new FakeBlogRepository();
            _controller = new BlogController(_repository);
        }

        [Fact]
        public async Task Index_ReturnsViewWithBlogs()
        {
            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Blog>>(result.Model);
        }

        [Fact]
        public void Post_GET_ReturnsViewWithNewBlogModel()
        {
            // Act
            var result = _controller.Post() as ViewResult;
            var model = result.Model as Blog;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.NotNull(model.BlogAuthor);
        }

        [Fact]
        public void Post_POST_StoresBlogAndRedirects()
        {
            // Arrange
            var blog = new Blog
            {
                BlogTitle = "Test Blog",
                BlogText = "Test Content",
                BlogAuthor = new AppUser { Name = "Test Author" }
            };

            // Act
            var result = _controller.Post(blog) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}