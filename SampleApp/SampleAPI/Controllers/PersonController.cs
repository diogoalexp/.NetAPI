using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleModel.DTO;
using SampleModel.Entities;
using SampleModel.Helper;
using SampleService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly ILogger<PersonController> _logger;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, ILogger<PersonController> logger, IMapper mapper)
        {
            this.personService = personService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get()
        {
            try
            {
                IEnumerable<Person> result = await personService.GetAsync();

                return Ok(_mapper.Map<IEnumerable<PersonDTO>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            try
            {
                Person person = await personService.GetFirstAsync(id);

                if (person is null)
                    return NotFound();

                return Ok(_mapper.Map<PersonDTO>(person));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PersonDTO>> Post(CreatePersonDTO model)
        {
            try
            {
                Person person = await personService.InsertAsync(_mapper.Map<Person>(model));

                if (person is null)
                    return BadRequest();

                return CreatedAtAction(nameof(Get), new { Id = person.Id }, _mapper.Map<PersonDTO>(person));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<PersonDTO>> Put(int id, UpdatePersonDTO model)
        {
            try
            {
                Person person = await personService.GetFirstAsync(id);
                if (person is null)
                    return NotFound();

                person = _mapper.Map<Person>(model);
                person.Id = id;

                person = await personService.UpdateAsync(person);

                return AcceptedAtAction(nameof(Get), new { Id = person.Id }, _mapper.Map<PersonDTO>(person));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<PersonDTO>> Delete(int id)
        {
            try
            {
                Person person = await personService.GetFirstAsync(id);
                if (person is null)
                    return NotFound();

                person = await personService.DeleteAsync(person);

                return AcceptedAtAction(nameof(Get), new { Id = person.Id }, _mapper.Map<PersonDTO>(person));
            }
            catch (Exception ex)
            {
                return BadRequest(_logger.ToLogError(ex));
            }
        }
    }
}
