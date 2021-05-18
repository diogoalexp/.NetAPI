using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleModel.DTO;
using SampleService.Services.Interfaces;
using System;
using Xunit;

namespace SampleTest.Controller
{
    public class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ILogger<AuthController>> _logger;

        public AuthControllerTest()
        {
            _authService = new Mock<IAuthService>();
            _logger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController
                (
                    _authService.Object,
                    _logger.Object
                );
        }

        [Fact]
        [Trait("Controller", "Auth")]
        public void SignIn()
        {
            //Arrange
            var result = Builder<AuthResponseDTO>.CreateNew().Build();

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            _authService
                .Setup(o => o.SignIn(It.IsAny<AuthRequestDTO>()))
                .ReturnsAsync(result);


            //Act
            var response = _controller.SignIn(request).Result;
            var resultDto = (response.Result as OkObjectResult).Value as AuthResponseDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<AuthResponseDTO>(resultDto);
        }

        [Fact]
        [Trait("Controller", "Auth")]
        public void SignUp()
        {
            //Arrange
            var result = Builder<RegisterResponseDTO>.CreateNew().Build();

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            _authService
                .Setup(o => o.SignUp(It.IsAny<AuthRequestDTO>()))
                .ReturnsAsync(result);


            //Act
            var response = _controller.SignUp(request).Result;
            var resultDto = (response.Result as OkObjectResult).Value as RegisterResponseDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<RegisterResponseDTO>(resultDto);
        }
    }
}
