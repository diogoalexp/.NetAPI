using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace SampleTest.Controller
{
    public class PersonControllerTest
    {
        private readonly PersonController _controller;
        private readonly Mock<IPersonService> _personService;
        private readonly Mock<ILogger<PersonController>> _logger;
        private readonly Mock<IMapper> _mapper;

        public PersonControllerTest()
        {
            _personService = new Mock<IPersonService>();
            _logger = new Mock<ILogger<PersonController>>();
            _mapper = new Mock<IMapper>();
            _controller = new PersonController
                (
                    _personService.Object,
                    _logger.Object,
                    _mapper.Object
                );
        }

        [Fact]
        [Trait("Controller", "Person")]
        public void Get()
        {
            //Arrange
            var result = Builder<Person>
                .CreateListOfSize(3)
                .TheFirst(1)
                .Build();

            var personDtoList = Builder<PersonDTO>
                  .CreateListOfSize(3)
                  .TheFirst(1)
                  .Build();

            _personService
                .Setup(o => o.GetAsync())
                .ReturnsAsync(result);
            _mapper
                .Setup(o => o.Map<IEnumerable<PersonDTO>>(It.IsAny<IEnumerable<Person>>()))
                .Returns(personDtoList);

            //Act
            var response = _controller.Get().Result;
            var resultDto = (response.Result as OkObjectResult).Value as IEnumerable<PersonDTO>;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<List<PersonDTO>>(resultDto);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [Trait("Controller", "Person")]
        public void Get_Id(int id)
        {
            //Arrange
            var result = Builder<Person>
                .CreateNew()
                .Build();

            var personDto = Builder<PersonDTO>
                  .CreateNew()
                  .Build();

            _personService
                .Setup(o => o.GetFirstAsync(It.IsAny<int>()))
                .ReturnsAsync(result);
            _mapper
                .Setup(o => o.Map<PersonDTO>(It.IsAny<Person>()))
                .Returns(personDto);

            //Act
            var response = _controller.Get(id).Result;
            var resultDto = (response.Result as OkObjectResult).Value as PersonDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<PersonDTO>(resultDto);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Controller", "Person")]
        public void Get_Id_NotFound(int id)
        {
            //Arrange
            _personService
                .Setup(o => o.GetFirstAsync(It.IsNotIn<int>(1)));

            //Act
            var response = _controller.Get(id).Result;
            var resultCode = (response.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultCode);
        }

        [Fact]
        [Trait("Controller", "Person")]
        public void Post()
        {
            //Arrange
            var result = Builder<Person>
                .CreateNew()
                .Build();

            var request = Builder<CreatePersonDTO>
                .CreateNew()
                .Build();

            var personDTO = Builder<PersonDTO>
                .CreateNew()
                .Build();


            _personService
                .Setup(o => o.InsertAsync(It.IsAny<Person>()))
                .ReturnsAsync(result);
            _mapper
                .Setup(o => o.Map<Person>(It.IsAny<CreatePersonDTO>()))
                .Returns(result);
            _mapper
                .Setup(o => o.Map<PersonDTO>(It.IsAny<Person>()))
                .Returns(personDTO);

            //Act
            var response = _controller.Post(request).Result;
            var resultDto = (response.Result as CreatedAtActionResult).Value as PersonDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<PersonDTO>(resultDto);
        }

        [Fact]
        [Trait("Controller", "Person")]
        public void Post_BadRequest()
        {
            //Arrange
            var result = Builder<Person>
                .CreateNew()
                .Build();

            var request = Builder<CreatePersonDTO>
                .CreateNew()
                .Build();

            var personDTO = Builder<PersonDTO>
                .CreateNew()
                .Build();


            _personService
                .Setup(o => o.InsertAsync(It.IsAny<Person>()));

            _mapper
                .Setup(o => o.Map<Person>(It.IsAny<CreatePersonDTO>()))
                .Returns(result);
            _mapper
                .Setup(o => o.Map<PersonDTO>(It.IsAny<Person>()))
                .Returns(personDTO);

            //Act
            var response = _controller.Post(request).Result;
            var resultCode = (response.Result as BadRequestResult).StatusCode;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultCode);
        }

        [Fact]
        [Trait("Controller", "Person")]
        public void Put()
        {
            //Arrange
            var result = Builder<Person>
                .CreateNew()
                .Build();

            var request = Builder<UpdatePersonDTO>
                .CreateNew()
                .Build();

            var personDTO = Builder<PersonDTO>
                .CreateNew()
                .Build();

            _personService
                .Setup(o => o.GetFirstAsync(It.IsAny<int>()))
                .ReturnsAsync(result);
            _personService
                .Setup(o => o.UpdateAsync(It.IsAny<Person>()))
                .ReturnsAsync(result);
            _mapper
                .Setup(o => o.Map<Person>(It.IsAny<UpdatePersonDTO>()))
                .Returns(result);
            _mapper
                .Setup(o => o.Map<PersonDTO>(It.IsAny<Person>()))
                .Returns(personDTO);

            //Act
            var response = _controller.Put(1, request).Result;
            var resultDto = (response.Result as AcceptedAtActionResult).Value as PersonDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<PersonDTO>(resultDto);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Controller", "Person")]
        public void Put_NotFound(int id)
        {
            //Arrange
            var request = Builder<UpdatePersonDTO>
                .CreateNew()
                .Build();

            _personService
                .Setup(o => o.GetFirstAsync(It.IsNotIn<int>(id)));

            //Act
            var response = _controller.Put(id, request).Result;
            var resultCode = (response.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultCode);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Controller", "Person")]
        public void Delete(int id)
        {
            //Arrange
            var result = Builder<Person>
                .CreateNew()
                .Build();

            var personDTO = Builder<PersonDTO>
                .CreateNew()
                .Build();

            _personService
                .Setup(o => o.GetFirstAsync(It.IsAny<int>()))
                .ReturnsAsync(result);
            _personService
                .Setup(o => o.DeleteAsync(It.IsAny<Person>()))
                .ReturnsAsync(result);

            _mapper
                .Setup(o => o.Map<PersonDTO>(It.IsAny<Person>()))
                .Returns(personDTO);

            //Act
            var response = _controller.Delete(id).Result;
            var resultDto = (response.Result as AcceptedAtActionResult).Value as PersonDTO;

            //Assert
            Assert.NotNull(resultDto);
            Assert.IsType<PersonDTO>(resultDto);
        }

        [Theory]
        [InlineData(1)]
        [Trait("Controller", "Person")]
        public void Delete_NotFound(int id)
        {
            //Arrange
            _personService
                .Setup(o => o.GetFirstAsync(It.IsNotIn<int>(id)));

            //Act
            var response = _controller.Delete(id).Result;
            var resultCode = (response.Result as NotFoundResult).StatusCode;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultCode);
        }
    }
}
