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
    public class RoomsRepository : RepositoryBase<Rooms>, IRoomsRepository
    {
        public RoomsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task CreateRoomsAsync(Rooms room)
        {
            Create(room);
            await SaveAsync();
        }

        public async Task ModifyRoomsAsync(Rooms room)
        {
            Update(room);
            await SaveAsync();
        }

        public async Task DeleteRoomsAsync(Rooms room)
        {
            Delete(room);
            await SaveAsync();
        }

        public async Task<IEnumerable<object>> GetAllRoomsAsync()
        {
            return await FindAll()
                .Select(p => new {
                    p.Id,
                    p.Name
                }).ToListAsync();
        }

        public async Task<Rooms> GetRoomsByIdAsync(int id)
        {
            return await FindByCondition(rooms => rooms.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Rooms> GetRoomsByNameAsync(string name)
        {
            return await FindByCondition(rooms => rooms.Name == name).FirstOrDefaultAsync();
        }

        
    }
}
