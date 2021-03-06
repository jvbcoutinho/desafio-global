using System;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Domain.UserAggregate;
using Desafio.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Desafio.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task Create(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await this._context.User.Where(x => x.Id == id).Include(x => x.Phones).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await this._context.User.Where(x => x.Email == email).Include(x => x.Phones).FirstOrDefaultAsync();
        }


    }
}