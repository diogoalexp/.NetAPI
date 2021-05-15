using SampleModel.Entities;
using SampleModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel.DTO
{

    public class AuthDTO
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }

    public class AuthRequestDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
    }

    public class AuthResponseDTO
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public Roles Role { get; set; }
        public string Token { get; set; }
    }

    public class RegisterResponseDTO
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public Roles Role { get; set; }
    }
}
