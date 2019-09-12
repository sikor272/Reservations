using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TeachersRepository : RepositoryBase<Teachers>, ITeachersRepository
    {
        public TeachersRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task CreateTeachersAsync(Teachers teacher)
        {
            Create(teacher);
            await SaveAsync();
        }

        public async Task ModifyTeachersAsync(Teachers teacher)
        {
            Update(teacher);
            await SaveAsync();
        }

        public async Task DeleteTeachersAsync(Teachers teacher)
        {
            Delete(teacher);
            await SaveAsync();
        }

        public async Task<IEnumerable<object>> GetAllTeachersAsync()
        {
            return await FindAll()
                 .Select(p => new {
                     p.Id,
                     p.Name,
                     p.Surname,
                     p.Title
                 }).ToListAsync();
        }

        public async Task<Teachers> GetTeachersByIdAsync(int id)
        {
            return await FindByCondition(Teachers => Teachers.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Teachers[]> GetTeachersByNameAsync(string name)
        {
            return await FindByCondition(Teachers => Teachers.Name == name).DefaultIfEmpty(new Teachers()).ToArrayAsync();
        }

        public async Task<Teachers[]> GetTeachersBySurnameAsync(string surname)
        {
            return await FindByCondition(Teachers => Teachers.Surname.Equals(surname)).DefaultIfEmpty(new Teachers()).ToArrayAsync();
        }

        
    }
}
