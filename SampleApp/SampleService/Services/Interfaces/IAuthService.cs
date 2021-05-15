using SampleModel.DTO;
using SampleModel.Entities;
using SampleService.Services.Base;
using System.Threading.Tasks;

namespace SampleService.Services.Interfaces
{
    public interface IAuthService : IBaseService<Auth>
    {
        Task<AuthResponseDTO> SignIn(AuthRequestDTO model);
        Task<RegisterResponseDTO> SignUp(AuthRequestDTO model);
    }
}
