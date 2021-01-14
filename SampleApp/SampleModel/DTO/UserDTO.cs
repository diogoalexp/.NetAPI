using SampleModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
    }

    public class UserResponseDTO
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public Roles Role { get; set; }

    }

    public class CreateUserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
