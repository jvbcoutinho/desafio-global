using System.ComponentModel.DataAnnotations;

namespace Desafio.Api.Dto
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email é um campo obrigátorio")]
        [EmailAddress(ErrorMessage = "Email não está em um formato correto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é um campo obrigátorio")]
        public string Password { get; set; }
    }
}