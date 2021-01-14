using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.Domain.UserAggregate.ValueObject;
using Desafio.Shared.Dto;

namespace Desafio.Domain.UserAggregate
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public IList<Phone> Phones { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime LastLogin { get; private set; }
        public string Token { get; private set; }

        public User(string name, string email, string password, IList<PhoneDto> phones)
        {
            Name = name;
            Email = email;
            Password = password;
            Phones = phones.Select(x => new Phone(x.Number, x.Ddd)).ToList();
            Created = DateTime.Now;
            Modified = Created;
            LastLogin = Created;
        }

        private User()
        {
        }

        public void UpdateToken(string token)
        {
            Token = token;
        }

        public void Login()
        {
            LastLogin = DateTime.Now;
        }
    }
}