using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Desafio.Shared.Dto;

namespace Desafio.Api.Authentication.Dto
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Nome é um campo obrigátorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigátorio")]
        [EmailAddress(ErrorMessage = "Email não está em um formato correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigátorio")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "A Senha deve ter no mínimo 8 caracteres, uma letra, um caracter especial e um número")]
        public string Password { get; set; }

        public IList<PhoneDto> Phones { get; set; }
    }
}