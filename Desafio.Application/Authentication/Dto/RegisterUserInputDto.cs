using System.Collections.Generic;
using Desafio.Shared.Dto;

namespace Desafio.Application.Authentication.Dto
{
    public class RegisterUserInputDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<PhoneDto> Phones { get; set; }
    }
}