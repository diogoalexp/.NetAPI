using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleModel.Helper;
using SampleService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            this.personService = personService;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get()
        {
            try
            {
                return Ok(await personService.GetAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            try
            {
                var person = await personService.GetFirstAsync(new Person() { Id = id });

                if (person is null)
                    return NotFound();

                return Ok(person.asDto());
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<PersonDTO>> Post(CreatePersonDTO personDTO)
        {
            try
            {
                Person person = await personService.InsertAsync(personDTO.asDomain());

                if (person is null)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { Id = person.Id }, person.asDto());
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ActionResult<PersonDTO>> Put(int id, UpdatePersonDTO personDTO)
        {
            try
            {
                var person = await personService.GetFirstAsync(new Person() { Id = id });
                if (person is null)
                    return NotFound();

                person.FirstName = personDTO.FirstName;
                person.LastName = personDTO.LastName;
                person.Age = personDTO.Age;

                person = await personService.UpdateAsync(person);

                return CreatedAtAction(nameof(Get), new { Id = person.Id }, person.asDto());
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<ActionResult<PersonDTO>> Delete(int id)
        {
            try
            {
                var person = await personService.GetFirstAsync(new Person() { Id = id });
                if (person is null)
                    return NotFound();

                await personService.DeleteAsync(person);

                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }
    }
}
