using SampleModel.DTO;
using SampleModel.Entities;
using SampleModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel
{
    public static class Extensions
    {
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

        public static UserResponseDTO asDto(this User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role,
            };
        }
    }
}
