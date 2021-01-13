using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desafio.Domain.UserAggregate;
using Desafio.Repository.Context;

namespace Desafio.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public void Criar(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

    }
}