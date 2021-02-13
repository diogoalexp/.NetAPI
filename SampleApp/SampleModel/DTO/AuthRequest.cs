using SampleModel.Entities;
using SampleModel.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleModel.DTO
{
    public class AuthRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
    }

    public class AuthResponse
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public Roles Role { get; set; }
        public string Token { get; set; }
    }

    public class RegisterResponse
    {
        public int Id { get; init; }
        public string Login { get; set; }
        public Roles Role { get; set; }
    }

    public static class MoradorDTOExtension
    {
        public static AuthResponse asAuthResponse(this User user, string Token)
        {
            return new AuthResponse
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role,
                Token = Token
            };
        }

        public static RegisterResponse asRegisterResponse(this User user)
        {
            return new RegisterResponse
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role,
            };
        }

        public static User asDomain(this AuthRequest model)
        {
            return new User
            {
                Login = model.Login,
                Pass = model.Password,
            };
        }
    }
}
