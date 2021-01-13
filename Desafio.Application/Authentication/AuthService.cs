using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Application.Authentication.Dto;
using Desafio.Domain.UserAggregate;
using Desafio.Shared.Exception;

namespace Desafio.Application.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<LoginUserOutputDto> Login(LoginUserInputDto nome)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RegisterUserOutputDto> RegisterUser(RegisterUserInputDto dto)
        {
            var businessException = new BusinessException();

            //TODO: Alterar esta consulta para dapper
            if ((await _userRepository.GetOneByCriteria(x => x.Email == dto.Email)) != null)
                businessException.AddError("E-mail já existente");

            businessException.ValidateAndThrow();

            var user = new User(dto.Name, dto.Email, dto.Password, dto.Phones);

            var token = TokenService.GenerateToken(user);
            user.UpdateToken(token);

            await _userRepository.Criar(user);

            return _mapper.Map<RegisterUserOutputDto>(user);
        }
    }
}