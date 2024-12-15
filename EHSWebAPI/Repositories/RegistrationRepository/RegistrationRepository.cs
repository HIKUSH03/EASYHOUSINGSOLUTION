using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;

namespace EHSWebAPI.Repositories.RegistrationRepository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly EHSDbContext _eHSDbContext;
        public RegistrationRepository(EHSDbContext eHSDbContext)
        {
            this._eHSDbContext = eHSDbContext;
        }

        public bool RegisterUser(User user)
        {
            if (IsUserExists(user.UserName))
                return false;

            if (!ValidatePassword(user.Password))
                return false;
            
            _eHSDbContext.Users.Add(user);
            _eHSDbContext.SaveChanges();
            return true;
        }


        public bool IsUserExists(string username)
        {
            return _eHSDbContext.Users.Any(u => u.UserName == username);
        }
        public bool ValidatePassword(string password)
        {
            /*
             * At least 6 characters 
             * Includes at least one digit
             * Includes at least one alphabet character (either lowercase or uppercase)
             * Includes at least one special character
             */
            string pattern = @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[\W_]).{6,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
    }
}