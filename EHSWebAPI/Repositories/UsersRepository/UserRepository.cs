using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHSWebAPI.Repositories.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly EHSDbContext _eHSDbContext;

        public UserRepository(EHSDbContext eHSDbContext)
        {
            _eHSDbContext = eHSDbContext;
        }

        public User CreateUser(User user)
        {
            _eHSDbContext.Users.Add(user);
            _eHSDbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(string UserName)
        {
            var user = _eHSDbContext.Users.SingleOrDefault(us => us.UserName == UserName);
            if (user == null) return false;

            _eHSDbContext.Users.Remove(user);
            _eHSDbContext.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _eHSDbContext.Users.ToList();
        }

        public User GetUserByUserName(string UserName)
        {
            return _eHSDbContext.Users.SingleOrDefault(name => name.UserName == UserName);
        }

      

        public bool UserExists(string UserName)
        {
            return _eHSDbContext.Users.Any(user => user.UserName == UserName);
        }
    }
}