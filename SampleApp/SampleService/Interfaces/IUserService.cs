using SampleModel.DTO;
using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleService.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<AuthResponse> Authenticate(AuthRequest model);
        Task<RegisterResponse> Register(AuthRequest model);
    }
}
