using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IRoomsRepository : IRepositoryBase<Rooms>
    {
        Task<IEnumerable<object>> GetAllRoomsAsync();
        Task CreateRoomsAsync(Rooms room);
        Task ModifyRoomsAsync(Rooms room);
        Task DeleteRoomsAsync(Rooms room);
        Task<Rooms> GetRoomsByNameAsync(string name);
        Task<Rooms> GetRoomsByIdAsync(int id);
    }
}
