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
    public class SubjectsRepository : RepositoryBase<Subjects>, ISubjectsRepository
    {
        public SubjectsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task CreateSubjectsAsync(Subjects subject)
        {
            Create(subject);
            await SaveAsync();
        }

        public async Task ModifySubjectsAsync(Subjects subject)
        {
            Update(subject);
            await SaveAsync();
        }

        public async Task DeleteSubjectsAsync(Subjects subject)
        {
            Delete(subject);
            await SaveAsync();
        }

        public async Task<Subjects> GetSubjectsByNameAsync(string name)
        {
            return await FindByCondition(Subjects => Subjects.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> GetAllSubjectsAsync()
        {
            return await FindAll()
                .Select(p => new {
                    p.Id,
                    p.Name
                }).ToListAsync();
        }

        public async Task<Subjects> GetSubjectsByIdAsync(int id)
        {
            return await FindByCondition(Subjects => Subjects.Id.Equals(id)).FirstOrDefaultAsync();
        }

    }
}
