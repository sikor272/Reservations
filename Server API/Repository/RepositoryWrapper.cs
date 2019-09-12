using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IUsersRepository _users;
        private ITeachersRepository _teachers;
        private ISubjectsRepository _subjects;
        private IRoomsRepository _rooms;
        private IReservationsRepository _reservations;
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IUsersRepository Users
        {
            get {
                if(_users == null)
                {
                    _users = new UsersRepository(_repoContext);
                }
 
                return _users;
            }
        }

        public ITeachersRepository Teachers
        {
            get {
                if(_teachers == null)
                {
                    _teachers = new TeachersRepository(_repoContext);
                }
 
                return _teachers;
            }
        }

        public ISubjectsRepository Subjects
        {
            get
            {
                if (_subjects == null)
                {
                    _subjects = new SubjectsRepository(_repoContext);
                }

                return _subjects;
            }
        }
        public IRoomsRepository Rooms
        {
            get
            {
                if (_rooms == null)
                {
                    _rooms = new RoomsRepository(_repoContext);
                }

                return _rooms;
            }
        }

        public IReservationsRepository Reservations
        {
            get
            {
                if (_reservations == null)
                {
                    _reservations = new ReservationsRepository(_repoContext);
                }

                return _reservations;
            }
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
