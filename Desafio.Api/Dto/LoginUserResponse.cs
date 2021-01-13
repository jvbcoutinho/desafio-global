using System;
using System.Collections.Generic;

namespace Desafio.Api.Dto
{
    public class LoginUserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<Phone> Phones { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
    }
}