using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Xunit;
using AnimeInfo.Controllers;
using AnimeInfo.Models;
using Moq;
using Microsoft.AspNetCore.Http;

namespace AnimeInfo.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<SignInManager<AppUser>> _mockSignInManager;

        public AccountControllerTests()
        {
  
            var userStoreMock = new Mock<IUserStore<AppUser>>();
            _mockUserManager = new Mock<UserManager<AppUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

          
            _mockSignInManager = new Mock<SignInManager<AppUser>>(
                _mockUserManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
                null, null, null, null);
        }

        [Fact]
        public void Register_GET_ReturnsView()
        {
       
            var controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object);

    
            var result = controller.Register() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Register_POST_InvalidModel_ReturnsView()
        {
        
            var controller = new AccountController(_mockUserManager.Object, _mockSignInManager.Object);
            var model = new RegisterViewModel();
            controller.ModelState.AddModelError("", "Test error");

        
            var result = await controller.Register(model) as ViewResult;

       
            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}