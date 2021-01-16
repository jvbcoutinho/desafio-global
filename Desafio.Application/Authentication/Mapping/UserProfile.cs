using AutoMapper;
using Desafio.Application.Authentication.Dto;
using Desafio.Domain.UserAggregate;
using Desafio.Shared.Dto;

namespace Desafio.Application.Authentication.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Phone, PhoneDto>();
            CreateMap<User, UserOutputDto>()
                .ForMember(d => d.Phones, o => o.MapFrom(s => s.Phones));

        }
    }
}