using System;
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

        public async Task<LoginUserOutputDto> Login(LoginUserInputDto dto)
        {

            var user = await _userRepository.GetByEmail(dto.Email);

            if (user == null)
                throw new NotFoundException("Usuário e/ou senha inválidos");

            if (user.Password != dto.Password)
                throw new AuthenticationException("Usuário e/ou senha inválidos");

            user.Login();

            await _userRepository.Update(user);

            return _mapper.Map<LoginUserOutputDto>(user);
        }

        public async Task<RegisterUserOutputDto> RegisterUser(RegisterUserInputDto dto)
        {

            if ((await _userRepository.GetByEmail(dto.Email)) != null)
                throw new BusinessException("E-mail já existente");

            var user = new User(dto.Name, dto.Email, dto.Password, dto.Phones);

            var token = TokenService.GenerateToken(user);
            user.UpdateToken(token);

            await _userRepository.Create(user);

            return _mapper.Map<RegisterUserOutputDto>(user);
        }

        public async Task<RegisterUserOutputDto> GetById(Guid id, string token)
        {
            if (!TokenService.ValidateToken(token))
                throw new AuthenticationException("Não autorizado");

            var user = await _userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException("Usuário não encontrado");

            if (user.Token != token)
                throw new AuthenticationException("Não autorizado");

            if (((DateTime.Now - user.LastLogin).TotalMinutes) > 30)
                throw new AuthenticationException("Sessão inválida");

            return _mapper.Map<RegisterUserOutputDto>(user);
        }
    }
}