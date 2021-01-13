using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Desafio.Domain.UserAggregate
{
    public interface IUserRepository
    {
        void Criar(User user);
    }
}