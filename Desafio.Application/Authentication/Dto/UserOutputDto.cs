using System;
using System.Collections.Generic;
using Desafio.Shared.Dto;

namespace Desafio.Application.Authentication.Dto
{
    public class UserOutputDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<PhoneDto> Phones { get; set; }
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
    }
}