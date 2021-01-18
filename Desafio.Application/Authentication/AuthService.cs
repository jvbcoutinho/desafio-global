using System;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Application.Authentication.Dto;
using Desafio.Domain.UserAggregate;
using Desafio.Shared.Exception;
using Desafio.Shared.Utils;
using Microsoft.AspNetCore.Identity;

namespace Desafio.Application.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserOutputDto> Login(LoginUserInputDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            if (!result.Succeeded)
                throw new AuthenticationException("Usuário e/ou senha inválidos");

            var user = await _userManager.FindByEmailAsync(dto.Email);

            user.Login();

            await _userManager.UpdateAsync(user);

            var outputDto = _mapper.Map<UserOutputDto>(user);
            outputDto.Token = TokenService.GenerateToken(dto.Email);

            return outputDto;
        }

        public async Task<UserOutputDto> RegisterUser(RegisterUserInputDto dto)
        {

            if ((await _userManager.FindByEmailAsync(dto.Email)) != null)
                throw new BusinessException("E-mail já existente");

            var token = TokenService.GenerateToken(dto.Email);

            var user = new User(dto.Name, dto.Email, dto.Phones, token);

            await _userManager.CreateAsync(user, dto.Password);

            await _signInManager.SignInAsync(user, false);
            var outputDto = _mapper.Map<UserOutputDto>(user);
            outputDto.Token = token;

            return outputDto;

        }

        public async Task<UserOutputDto> GetById(string id, string token)
        {
            if (!TokenService.ValidateToken(token))
                throw new AuthenticationException("Não autorizado");

            var user = await _userManager.FindByIdAsync(id);

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