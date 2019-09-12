using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITeachersRepository : IRepositoryBase<Teachers>
    {
        Task<IEnumerable<object>> GetAllTeachersAsync();
        Task CreateTeachersAsync(Teachers teacher);
        Task ModifyTeachersAsync(Teachers teacher);
        Task DeleteTeachersAsync(Teachers teacher);
        Task<Teachers[]> GetTeachersByNameAsync(string name);
        Task<Teachers[]> GetTeachersBySurnameAsync(string surname);
        Task<Teachers> GetTeachersByIdAsync(int id);
    }
}
