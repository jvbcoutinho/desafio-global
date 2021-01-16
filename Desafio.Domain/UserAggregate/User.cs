using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.Domain.UserAggregate.ValueObject;
using Desafio.Shared.Dto;
using Desafio.Shared.Utils;

namespace Desafio.Domain.UserAggregate
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime LastLogin { get; private set; }
        public string Token { get; private set; }
        public IList<Phone> Phones { get; set; }

        public User(string name, string email, string password, IList<PhoneDto> phones, string token)
        {
            Name = name;
            Email = email;
            Password = SecurityUtils.HashSHA1(password);
            Phones = phones?.Select(x => new Phone(x.Number, x.Ddd)).ToList();
            Created = DateTime.Now;
            Modified = Created;
            LastLogin = Created;
            Token = token;
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