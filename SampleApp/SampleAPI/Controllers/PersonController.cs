using Microsoft.AspNetCore.Mvc;
using SampleDAL.Repositories;
using SampleModel;
using SampleModel.DTO;
using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository repository;

        public PersonController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<PersonDTO> Get()
        {
            return repository.GetPersons().Select(p => p.asDto());
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public ActionResult<PersonDTO> Get(int id)
        {
            var person = repository.GetPerson(id);

            if (person is null)
                return NotFound();

            return Ok(person.asDto());
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult<PersonDTO> Post(CreatePersonDTO PersonDTO)
        {
            Person person = new()
            {
                //id = Guid.NewGuid(),
                FirstName = PersonDTO.FirstName,
                LastName = PersonDTO.LastName,
                Age = PersonDTO.Age,
            };
            repository.CreatePerson(person);

            return CreatedAtAction(nameof(Get), new { Id = person.Id }, person.asDto());
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, UpdatePersonDTO PersonDTO)
        {
            var existingPerson = repository.GetPerson(id);
            if (existingPerson is null)
                return NotFound();


            existingPerson.FirstName = PersonDTO.FirstName;
            existingPerson.LastName = PersonDTO.LastName;
            existingPerson.Age = PersonDTO.Age;

            repository.UpdatePerson(existingPerson);

            return NoContent();

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingPerson = repository.GetPerson(id);
            if (existingPerson is null)
                return NotFound();

            repository.DeletePerson(existingPerson.Id);

            return NoContent();
        }
    }
}
