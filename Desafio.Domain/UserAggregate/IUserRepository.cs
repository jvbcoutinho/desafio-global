using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Desafio.Domain.UserAggregate
{
    public interface IUserRepository
    {
        Task Criar(User user);
        Task<User> GetOneByCriteria(Expression<Func<User, bool>> expression);
    }
}