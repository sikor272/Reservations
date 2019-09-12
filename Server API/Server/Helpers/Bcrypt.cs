using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace Server.Helpers
{
    /// <summary>
    /// Class for crypting and decrypting users password
    /// </summary>
    public class Bcrypt
    {
        private IRepositoryWrapper _repository;
        public Bcrypt(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public string Crypting(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool Authorisation(string password, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }
        public async Task<Users> AuthorisationUsers(Users login)
        {
            Users user = null;
            string hashed = login.Password;
            try
            {
                user = await _repository.Users.GetUsersByUsernameAsync(login.Name);
                if (Authorisation(hashed, user.Password))
                {
                    return user;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
