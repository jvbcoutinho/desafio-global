using System;
using System.Collections.Generic;
using Desafio.Domain.UserAggregate.ValueObject;

namespace Desafio.Domain.UserAggregate
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public IList<Phone> Phones { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime LastLogin { get; private set; }
        public string Token { get; private set; }

        public User(string name, Email email, Password password, IList<Phone> phones, DateTime created, DateTime modified, DateTime lastLogin, string token)
        {
            Name = name;
            Email = email;
            Password = password;
            Phones = phones;
            Created = created;
            Modified = modified;
            LastLogin = lastLogin;
            Token = token;
        }
    }
}