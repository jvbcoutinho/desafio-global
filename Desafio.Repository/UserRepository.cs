using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Desafio.Domain.UserAggregate;
using Desafio.Repository.Context;
using Microsoft.Data.SqlClient;
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
            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                try
                {
                    connection.Open();
                    var sql = @"SELECT * FROM [Users]
                                LEFT JOIN [Phones] ON [Users].[Id] = [Phones].[UserId]
                                WHERE [Users].[Id] = @Id";
                    var user = await connection.QueryAsync<User, Phone, User>(sql, (u, p) =>
                                {
                                    u.Phones = new List<Phone>();
                                    u.Phones.Add(p);
                                    return u;
                                },
                                new { Id = id },
                                splitOn: "UserId"
                            );
                    return user.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("connectionString")))
            {
                try
                {
                    connection.Open();
                    var sql = @"SELECT * FROM [USERS] WHERE Email = @Email";
                    var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }


    }
}