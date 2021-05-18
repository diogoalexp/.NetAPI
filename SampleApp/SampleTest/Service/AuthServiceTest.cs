using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.Extensions.Configuration;
using Moq;
using SampleDAL.Repositories.Interfaces;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleModel.Constants;
using SampleService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleTest.Service
{
    public class AuthServiceTest
    {
        private readonly AuthService _service;
        private readonly Mock<IAuthRepository> _authRepository;
        private readonly Mock<IConfiguration> _config;
        private readonly Mock<IMapper> _mapper;

        public AuthServiceTest()
        {
            _authRepository = new Mock<IAuthRepository>();
            _config = new Mock<IConfiguration>();
            _mapper = new Mock<IMapper>();
            _service = new AuthService
                (
                    _authRepository.Object,
                    _config.Object,
                    _mapper.Object
                );
        }

        [Fact]
        [Trait("Service", "Auth")]
        public void SignIn()
        {
            //Arrange
            var result = Builder<AuthResponseDTO>.CreateNew().Build();

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            var auth = Builder<Auth>.CreateNew().Build();
            var authList = Builder<Auth>
                .CreateListOfSize(1)
                .TheFirst(1)
                .With(w => w.Pass = "test_password")
                .Build();

            _mapper
                .Setup(o => o.Map<Auth>(It.IsAny<AuthRequestDTO>()))
                .Returns(auth);
            _mapper
                .Setup(o => o.Map<AuthResponseDTO>(It.IsAny<Auth>()))
                .Returns(result);

            _config
                .Setup(o => o[It.IsAny<string>()])
                .Returns("tests9d8534b48w951b9287d492b256x");

            _authRepository
                .Setup(o => o.GetAsync(It.IsAny<Auth>(), It.IsAny<Expression<Func<Auth, bool>>>(), null))
                .ReturnsAsync(authList);


            //Act
            var response = _service.SignIn(request).Result;
            var resultDto = response as AuthResponseDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.Equal(result, resultDto);
            Assert.IsType<AuthResponseDTO>(resultDto);
        }

        [Fact]
        [Trait("Service", "Auth")]
        public void SignIn_InvalidLogin()
        {
            //Arrange
            var exception = string.Empty;

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = string.Empty)
                .With(w => w.Password = "test_password")
                .Build();

            //Act
            try
            {
                var response = _service.SignIn(request).Result;
            }
            catch (Exception ex)
            {
                exception = ex.Message;
            }

            //Assert
            Assert.Contains(Constants.Error.INVALID_LOGIN, exception);
        }

        [Fact]
        [Trait("Service", "Auth")]
        public void SignIn_InvalidUser()
        {
            //Arrange
            var exception = string.Empty;

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            var auth = Builder<Auth>.CreateNew().Build();

            _mapper
                .Setup(o => o.Map<Auth>(It.IsAny<AuthRequestDTO>()))
                .Returns(auth);

            _authRepository
                .Setup(o => o.GetAsync(It.IsAny<Auth>(), It.IsAny<Expression<Func<Auth, bool>>>(), null));

            //Act
            try
            {
                var response = _service.SignIn(request).Result;
            }
            catch (Exception ex)
            {
                exception = ex.Message;
            }

            //Assert
            Assert.Contains(Constants.Error.INVALID_USER, exception);
        }

        [Fact]
        [Trait("Service", "Auth")]
        public void SignIn_InvalidPassword()
        {
            //Arrange
            var exception = string.Empty;

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            var auth = Builder<Auth>.CreateNew().Build();
            var authList = Builder<Auth>
                .CreateListOfSize(1)
                .TheFirst(1)
                .With(w => w.Pass = "test_password_wrong")
                .Build();

            _mapper
                .Setup(o => o.Map<Auth>(It.IsAny<AuthRequestDTO>()))
                .Returns(auth);

            _authRepository
                .Setup(o => o.GetAsync(It.IsAny<Auth>(), It.IsAny<Expression<Func<Auth, bool>>>(), null))
                .ReturnsAsync(authList);

            //Act
            try
            {
                var response = _service.SignIn(request).Result;
            }
            catch (Exception ex)
            {
                exception = ex.Message;
            }

            //Assert
            Assert.Contains(Constants.Error.INVALID_PASSWORD, exception);
        }

        [Fact]
        [Trait("Service", "Auth")]
        public void SignUp()
        {
            //Arrange
            var result = Builder<RegisterResponseDTO>.CreateNew().Build();

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            var auth = Builder<Auth>.CreateNew().Build();
            var authList = Builder<Auth>
                .CreateListOfSize(1)
                .TheFirst(1)
                .With(w => w.Pass = "test_password")
                .Build();

            _mapper
                .Setup(o => o.Map<Auth>(It.IsAny<AuthRequestDTO>()))
                .Returns(auth);
            _mapper
                .Setup(o => o.Map<RegisterResponseDTO>(It.IsAny<Auth>()))
                .Returns(result);

            _authRepository
                .Setup(o => o.InsertAsync(It.IsAny<Auth>()))
                .ReturnsAsync(auth);

            //Act
            var response = _service.SignUp(request).Result;
            var resultDto = response as RegisterResponseDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.Equal(result, resultDto);
            Assert.IsType<RegisterResponseDTO>(resultDto);
        }

        [Fact]
        [Trait("Service", "Auth")]
        public void SignUp_InvalidUserExists()
        {
            //Arrange
            var exception = string.Empty;

            var request = Builder<AuthRequestDTO>
                .CreateNew()
                .With(w => w.Login = "test_login")
                .With(w => w.Password = "test_password")
                .Build();

            var auth = Builder<Auth>.CreateNew().Build();

            var authList = Builder<Auth>
                .CreateListOfSize(1)
                .TheFirst(1)
                .With(w => w.Pass = "test_password")
                .Build();

            _mapper
                .Setup(o => o.Map<Auth>(It.IsAny<AuthRequestDTO>()))
                .Returns(auth);

            _authRepository
                .Setup(o => o.GetAsync(It.IsAny<Auth>(), It.IsAny<Expression<Func<Auth, bool>>>(), null))
                .ReturnsAsync(authList);

            //Act
            try
            {
                var response = _service.SignUp(request).Result;
            }
            catch (Exception ex)
            {
                exception = ex.Message;
            }

            //Assert
            Assert.Contains(Constants.Error.INVALID_USER_EXISTS, exception);
        }
    }
}
