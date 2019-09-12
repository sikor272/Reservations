using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IUsersRepository : IRepositoryBase<Users>
    {
        Task CreateUsersAsync(Users user);
        Task DeleteUsersAsync(Users user);
        Task<Users> GetUsersByUsernameAsync(string username);
        Task<Users> GetUsersByIdAsync(int id);
    }
}
