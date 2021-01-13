using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Desafio.Domain.UserAggregate;
using Desafio.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task Criar(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetOneByCriteria(Expression<Func<User, bool>> expression)
        {
            return await this._context.User.Where(expression).FirstOrDefaultAsync();
        }


    }
}