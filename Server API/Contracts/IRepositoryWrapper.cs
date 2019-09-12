using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUsersRepository Users { get; }
        ITeachersRepository Teachers { get; }
        ISubjectsRepository Subjects { get; }
        IRoomsRepository Rooms { get; }
        IReservationsRepository Reservations { get; }
        void Save();
    }
}
