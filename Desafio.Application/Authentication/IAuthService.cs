using System;
using System.Threading.Tasks;
using Desafio.Application.Authentication.Dto;

namespace Desafio.Application.Authentication
{
    public interface IAuthService
    {
        Task<UserOutputDto> RegisterUser(RegisterUserInputDto dto);
        Task<UserOutputDto> Login(LoginUserInputDto nome);
        Task<UserOutputDto> GetById(Guid id, string token);
    }
}