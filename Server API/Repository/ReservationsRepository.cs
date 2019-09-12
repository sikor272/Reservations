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
    public class ReservationsRepository : RepositoryBase<Reservations>, IReservationsRepository
    {
        public ReservationsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task CreateReservationsAsync(Reservations reservation)
        {
            Create(reservation);
            await SaveAsync();
        }

        public async Task ModifyReservationsAsync(Reservations modified)
        {
            Update(modified);
            await SaveAsync();
        }

        public async Task DeleteReservationsAsync(Reservations reservation)
        {
            Delete(reservation);
            await SaveAsync();
        }

        public async Task<IEnumerable<object>> GetAllReservationsAsync()
        {
            return await FindAll()
                 .Select(p => new {
                     p.Id,
                     p.Teacher_id,
                     p.Subject_id,
                     p.Room_id,
                     p.Date,
                     p.Begin,
                     p.End
                 }).ToListAsync();
        }

        public async Task<Reservations[]> GetReservationsByDateAsync(DateTime date)
        {
            return await FindByCondition(Reservations => Reservations.Date == date).DefaultIfEmpty(new Reservations()).ToArrayAsync();
        }

        public async Task<Reservations> GetReservationsByIdAsync(int id)
        {
            return await FindByCondition(Reservations => Reservations.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Reservations[]> GetReservationsBySubjectsIdAsync(int id)
        {
            return await FindByCondition(Reservations => Reservations.Subject_id == id).DefaultIfEmpty(new Reservations()).OrderBy(p => p.Date).ToArrayAsync();
        }

        public async Task<Reservations[]> GetReservationsByTeachersIdAsync(int id)
        {
            return await FindByCondition(Reservations => Reservations.Teacher_id == id).DefaultIfEmpty(new Reservations()).OrderBy(p => p.Date).ToArrayAsync();
        }

        public async Task<Reservations[]> GetReservationsByRoomsIdAsync(int id)
        {
            return await FindByCondition(Reservations => Reservations.Room_id == id).DefaultIfEmpty(new Reservations()).OrderBy(p => p.Date).ToArrayAsync();
        }

        public async Task<bool> CheckReservations(Reservations reservation)
        {
            int subject = await FindByCondition(Reservations =>
                                            Reservations.Subject_id == reservation.Subject_id &&
                                            Reservations.Date == reservation.Date &&
                                            Reservations.Begin < reservation.End &&
                                            Reservations.End > reservation.Begin).CountAsync();
            int teacher = await FindByCondition(Reservations =>
                                            Reservations.Teacher_id == reservation.Teacher_id &&
                                            Reservations.Date == reservation.Date &&
                                            Reservations.Begin < reservation.End &&
                                            Reservations.End > reservation.Begin).CountAsync();
            int room = await FindByCondition(Reservations =>
                                            Reservations.Room_id == reservation.Room_id &&
                                            Reservations.Date == reservation.Date &&
                                            Reservations.Begin < reservation.End &&
                                            Reservations.End > reservation.Begin).CountAsync();
            return subject + teacher + room == 0;
        }
        public async Task<bool> CheckReservationsUpdate(Reservations reservation)
        {
            int subject = await FindByCondition(Reservations =>
                                            Reservations.Subject_id == reservation.Subject_id &&
                                            Reservations.Date == reservation.Date &&
                                            Reservations.Id != reservation.Id &&
                                            Reservations.Begin < reservation.End &&
                                            Reservations.End > reservation.Begin).CountAsync();
            int teacher = await FindByCondition(Reservations =>
                                            Reservations.Teacher_id == reservation.Teacher_id &&
                                            Reservations.Date == reservation.Date &&
                                            Reservations.Id != reservation.Id &&
                                            Reservations.Begin < reservation.End &&
                                            Reservations.End > reservation.Begin).CountAsync();
            int room = await FindByCondition(Reservations =>
                                            Reservations.Room_id == reservation.Room_id &&
                                            Reservations.Date == reservation.Date &&
                                            Reservations.Id != reservation.Id &&
                                            Reservations.Begin < reservation.End &&
                                            Reservations.End > reservation.Begin).CountAsync();
            return subject + teacher + room == 0;
        }
    }
}
