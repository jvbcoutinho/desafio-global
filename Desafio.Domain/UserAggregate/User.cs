using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.Shared.Dto;
using Desafio.Shared.Utils;
using Microsoft.AspNetCore.Identity;

namespace Desafio.Domain.UserAggregate
{
    public class User : IdentityUser
    {
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime LastLogin { get; private set; }
        public string Token { get; private set; }
        public IList<Phone> Phones { get; set; }

        public User(string name, string email, IList<PhoneDto> phones, string token)
        {
            UserName = email;
            Name = name;
            Email = email;
            Phones = phones?.Select(x => new Phone(x.Number, x.Ddd)).ToList();
            Created = DateTime.Now;
            Modified = Created;
            LastLogin = Created;
            Token = SecurityUtils.HashSHA1(token); ;
        }

        private User()
        {
        }

        public void Login()
        {
            LastLogin = DateTime.Now;
        }
    }
}