using System;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Api.Authentication.Dto;
using Desafio.Application.Authentication.Dto;
using Desafio.Application.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            [FromServices] IAuthService authenticationService,
            [FromServices] IMapper _mapper
        )
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<IActionResult>(BadRequest(ModelState));

            var inputDto = _mapper.Map<RegisterUserInputDto>(request);

            var response = await authenticationService.RegisterUser(inputDto);

            return Created(string.Empty, response);
        }

        [HttpPost("Login")]
        public Task<IActionResult> Login(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
