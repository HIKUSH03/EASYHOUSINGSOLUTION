using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.DTOs;

namespace EHSWebAPI.Services
{
    public class AuthServices
    {
        private readonly EHSDbContext _context;
        public AuthServices(EHSDbContext context)
        {
            _context = context;
        }

        //Register a New user
        public string Register(RegisterDto registerDto)
        {
            if (_context.Users.Any(u => u.UserName == registerDto.UserName))
            {
                return "User already exists";
            }
            var user = new User
            {
                UserName = registerDto.UserName,
                Password = HashPassword(registerDto.Password),
                UserType = registerDto.UserType
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return "User registered successfully";
        }

        //Authenticate user
        public (string, string) Authenticate(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName);
            if (user == null)
            {
                return ("Invalid credentials", null);
            }
            if (user.Password != HashPassword(loginDto.Password))
            {
                return ("Invalid credentials", null);
            }
            return ("Authentication successful", user.UserType.ToString());
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                string pass = Convert.ToBase64String(hashBytes);
                return pass.Substring(0, Math.Min(pass.Length, 24));
            }
        }
    }
}