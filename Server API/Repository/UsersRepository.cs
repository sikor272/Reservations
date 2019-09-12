using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task CreateUsersAsync(Users user)
        {
            Create(user);
            await SaveAsync();
        }

        public async Task DeleteUsersAsync(Users user)
        {
            Delete(user);
            await SaveAsync();
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            return await FindByCondition(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Users> GetUsersByUsernameAsync(string username)
        {
            return await FindByCondition(user => user.Name == username).FirstOrDefaultAsync();
        }
    }
}
