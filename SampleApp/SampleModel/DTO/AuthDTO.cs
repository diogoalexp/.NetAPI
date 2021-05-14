using SampleModel.Entities;
using SampleModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel.DTO
{
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

    public static class AuthDTOExtension
    {
        public static AuthResponseDTO asAuthResponse(this User user, string Token)
        {
            return new AuthResponseDTO
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role,
                Token = Token
            };
        }

        public static RegisterResponseDTO asRegisterResponse(this User user)
        {
            return new RegisterResponseDTO
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role,
            };
        }

        public static User asDomain(this AuthRequestDTO model)
        {
            return new User
            {
                Login = model.Login,
                Pass = model.Password,
            };
        }
    }
}
