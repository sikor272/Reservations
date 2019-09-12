using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ISubjectsRepository : IRepositoryBase<Subjects>
    {
        Task<IEnumerable<object>> GetAllSubjectsAsync();
        Task CreateSubjectsAsync(Subjects subject);
        Task ModifySubjectsAsync(Subjects subject);
        Task DeleteSubjectsAsync(Subjects subject);
        Task<Subjects> GetSubjectsByNameAsync(string name);
        Task<Subjects> GetSubjectsByIdAsync(int id);
    }
}
