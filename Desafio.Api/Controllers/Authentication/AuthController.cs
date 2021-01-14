using System;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Api.Authentication.Dto;
using Desafio.Application.Authentication.Dto;
using Desafio.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
            [FromServices] IMapper _mapper)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<IActionResult>(BadRequest(ModelState));

            var inputDto = _mapper.Map<RegisterUserInputDto>(request);

            var response = await authenticationService.RegisterUser(inputDto);

            return Created(string.Empty, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(
            LoginUserRequest request,
            [FromServices] IAuthService authenticationService,
            [FromServices] IMapper _mapper)
        {
            if (!ModelState.IsValid)
                return await Task.FromResult<IActionResult>(BadRequest(ModelState));

            var inputDto = _mapper.Map<LoginUserInputDto>(request);

            var response = await authenticationService.Login(inputDto);

            return Ok(response);
        }

        [HttpGet("")]
        [Authorize]
        public IActionResult Get(
            [FromServices] IAuthService authenticationService,
            [FromServices] IMapper _mapper)
        {
            //Todo: Ver forma mais elegante de obter o token
            var accessToken = Request.Headers["Authorization"].ToString();
            Console.WriteLine(accessToken.Split(' ')[1]);
            
            return Ok(new { message = "Teste" });
        }
    }
}
