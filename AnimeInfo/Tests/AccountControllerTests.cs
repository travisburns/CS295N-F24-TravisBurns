using Microsoft.AspNetCore.Mvc;
using Xunit;
using AnimeInfo.Controllers;
using AnimeInfo.Models;

namespace AnimeInfo.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void Register_GET_ReturnsView()
        {
      
            var controller = new AccountController(null, null);  

         
            var result = controller.Register() as ViewResult;

            Assert.NotNull(result); 
        }

        [Fact]
        public void Register_POST_InvalidModel_ReturnsView()
        {
           
            var controller = new AccountController(null, null);
            var model = new RegisterViewModel(); 
            controller.ModelState.AddModelError("", "Test error");

        
            var result = controller.Register(model).Result as ViewResult;

         
            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}