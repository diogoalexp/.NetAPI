using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel.DTO
{
    public class PersonDTO
    {
        public int Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        //public List<Address> Addresses { get; set; } = new List<Address>();
        //public List<Email> EmailAddresses { get; set; } = new List<Email>();
    }

    public class CreatePersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class UpdatePersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public static class PersonDTOExtension
    {

        public static Person asDomain(this CreatePersonDTO model)
        {
            return new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age
            };
        }

        public static Person asDomain(this UpdatePersonDTO model)
        {
            return new Person
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age
            };
        }

        public static PersonDTO asDto(this Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
            };
        }
    }
}
