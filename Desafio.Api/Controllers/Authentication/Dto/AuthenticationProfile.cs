using AutoMapper;
using Desafio.Api.Authentication.Dto;
using Desafio.Application.Authentication.Dto;

namespace Desafio.Api.Controllers.Authentication.Dto
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<RegisterUserRequest, RegisterUserInputDto>();
        }

    }
}