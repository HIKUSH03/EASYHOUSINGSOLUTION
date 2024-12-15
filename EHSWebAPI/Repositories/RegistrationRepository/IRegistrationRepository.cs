using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EHSDataAccessLayer.Entity;

namespace EHSWebAPI.Repositories.RegistrationRepository
{
    public interface IRegistrationRepository
    {
        bool RegisterUser(User user);
    }
}
