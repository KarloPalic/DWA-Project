using DAL.DataTransferObject;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DwaMoviesContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(DwaMoviesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                throw new InvalidOperationException($"User with ID {id} not found.");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByUsername(User user)
        {
            return _context.Users.FirstOrDefault(u => u.Username == user.Username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<bool> RegisterUser(UserRegistrationDTO userRegistrationDTO)
        {
            var user = new User
            {
                Username = userRegistrationDTO.Username,
                FirstName = userRegistrationDTO.FirstName,
                LastName = userRegistrationDTO.LastName,
                Email = userRegistrationDTO.Email,
                Phone = userRegistrationDTO.Phone,
                IsConfirmed = false
            };

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Hash the password using PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(userRegistrationDTO.Password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                user.PwdSalt = Convert.ToBase64String(salt);
                user.PwdHash = Convert.ToBase64String(hashBytes);
            }

            var countryName = userRegistrationDTO.CountryOfResidenceName; 
            var country = _context.Countries.FirstOrDefault(c => c.Name == countryName);

            if (country != null)
            {
                user.CountryOfResidenceId = country.Id;
            }
            else
            {
                throw new InvalidOperationException($"Country with name {countryName} not found.");
            }

            var confirmationToken = Guid.NewGuid().ToString();
            user.SecurityToken = confirmationToken;


            AddUser(user);

            return true;
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = _context.Users.Find(user.Id);

            if (existingUser == null)
                throw new InvalidOperationException($"User with ID {user.Id} not found.");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.PwdHash = user.PwdHash;
            existingUser.PwdSalt = user.PwdSalt;
            existingUser.SecurityToken = user.SecurityToken;
            existingUser.CountryOfResidenceId = user.CountryOfResidenceId;

            _context.SaveChanges();
        }
    }
}
