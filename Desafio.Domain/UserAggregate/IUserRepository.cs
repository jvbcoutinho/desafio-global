using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio.Domain.UserAggregate
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task Update(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
    }
}