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
        public string Register(RegisterBuyerDto registerDto)
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
            var buyer = new Buyer
            {
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                DateOfBirth = registerDto.DateOfBirth,
                PhoneNo = registerDto.PhoneNo,
                EmailId = registerDto.EmailId,

            };

            _context.Users.Add(user);
            _context.Buyers.Add(buyer);
            _context.SaveChanges();

            return "User registered successfully";
        }




        //REgister Seller

        public string Register(RegisterSellerDto registerSellerDto)
        {
            if (_context.Users.Any(u => u.UserName == registerSellerDto.UserName))
            {
                return "User already exists";
            }
            var user = new User
            {
                UserName = registerSellerDto.UserName,
                Password = HashPassword(registerSellerDto.Password),
                UserType = registerSellerDto.UserType
            };
            var seller = new Seller
            {
                UserName = registerSellerDto.UserName,
                FirstName = registerSellerDto.FirstName,
                LastName = registerSellerDto.LastName,
                DateOfBirth = registerSellerDto.DateOfBirth,
                PhoneNo = registerSellerDto.PhoneNo,
                Address = registerSellerDto.Address,
                StateId = registerSellerDto.StateId,
                CityId = registerSellerDto.CityId,
                EmailId = registerSellerDto.EmailId
            };
            try
            {

                _context.Users.Add(user);
                _context.Sellers.Add(seller);
                _context.SaveChanges();

                return "User registered successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        //Authenticate user
        //login
        public (string, string, string) Authenticate(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName);
            if (user == null)
            {
                return ("Invalid credentials", null, null);
            }
            if (user.Password != HashPassword(loginDto.Password))
            {
                return ("Invalid credentials", null, null);
            }
            return ("Authentication successful", user.UserType.ToString(), user.UserName);
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