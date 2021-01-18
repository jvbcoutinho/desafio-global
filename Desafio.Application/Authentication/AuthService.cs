using System;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Application.Authentication.Dto;
using Desafio.Domain.UserAggregate;
using Desafio.Shared.Exception;
using Desafio.Shared.Utils;

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

        public async Task<UserOutputDto> Login(LoginUserInputDto dto)
        {

            var user = await _userRepository.GetByEmail(dto.Email);

            if (user == null)
                throw new NotFoundException("Usuário e/ou senha inválidos");

            if (user.Password != SecurityUtils.HashSHA1(dto.Password))
                throw new AuthenticationException("Usuário e/ou senha inválidos");

            user.Login();

            await _userRepository.Update(user);

            var outputDto = _mapper.Map<UserOutputDto>(user);
            outputDto.Token = TokenService.GenerateToken(dto.Email);

            return outputDto;
        }

        public async Task<UserOutputDto> RegisterUser(RegisterUserInputDto dto)
        {

            if ((await _userRepository.GetByEmail(dto.Email)) != null)
                throw new BusinessException("E-mail já existente");

            var token = TokenService.GenerateToken(dto.Email);

            var user = new User(dto.Name, dto.Email, dto.Password, dto.Phones, token);

            await _userRepository.Create(user);

            var outputDto = _mapper.Map<UserOutputDto>(user);
            outputDto.Token = token;

            return outputDto;
        }

        public async Task<UserOutputDto> GetById(Guid id, string token)
        {
            if (!TokenService.ValidateToken(token))
                throw new AuthenticationException("Não autorizado");

            var user = await _userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException("Usuário não encontrado");

            if (user.Token != SecurityUtils.HashSHA1(token))
                throw new AuthenticationException("Não autorizado");

            if (((DateTime.Now - user.LastLogin).TotalMinutes) > 30)
                throw new AuthenticationException("Sessão inválida");

            var outputDto = _mapper.Map<UserOutputDto>(user);
            outputDto.Token = TokenService.GenerateToken(user.Email);

            return outputDto;
        }
    }
}