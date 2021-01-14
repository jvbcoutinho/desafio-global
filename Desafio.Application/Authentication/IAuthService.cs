using System;
using System.Threading.Tasks;
using Desafio.Application.Authentication.Dto;

namespace Desafio.Application.Authentication
{
    public interface IAuthService
    {
        Task<RegisterUserOutputDto> RegisterUser(RegisterUserInputDto dto);
        Task<LoginUserOutputDto> Login(LoginUserInputDto nome);

        Task<RegisterUserOutputDto> GetById(Guid id, string token);
    }
}