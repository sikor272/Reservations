using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
namespace Contracts
{
    public interface IReservationsRepository : IRepositoryBase<Reservations>
    {
        Task<IEnumerable<object>> GetAllReservationsAsync();
        Task CreateReservationsAsync(Reservations reservation);
        Task ModifyReservationsAsync(Reservations modified);
        Task DeleteReservationsAsync(Reservations reservation);
        Task<Reservations> GetReservationsByIdAsync(int id);
        Task<Reservations[]> GetReservationsByTeachersIdAsync(int id);
        Task<Reservations[]> GetReservationsBySubjectsIdAsync(int id);
        Task<Reservations[]> GetReservationsByRoomsIdAsync(int id);
        Task<Reservations[]> GetReservationsByDateAsync(DateTime date);
        Task<bool> CheckReservations(Reservations reservation);
        Task<bool> CheckReservationsUpdate(Reservations reservation);
    }
}
