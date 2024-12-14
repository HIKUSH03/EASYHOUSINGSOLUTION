using EHSDataAccessLayer.Entity;
using System.Collections.Generic;

namespace EHSWebAPI.Repositories.UsersRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUser();
        User CreateUser(User user);       
        bool DeleteUser(string user);
        User GetUserByUserName(string UserName);
        bool UserExists(string UserName);

    }
}
