using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.RegistrationRepository;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/registration")]
    public class UserRegistrationApiController : ApiController
    {
        private readonly IRegistrationRepository _registrationRepository;
        public UserRegistrationApiController()
        {
            EHSDbContext eHSDbContext = new EHSDbContext();
            _registrationRepository = new RegistrationRepository(eHSDbContext);
        }
        // register user
        [HttpPost]
        [Route("")]
        public IHttpActionResult RegisterUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _registrationRepository.RegisterUser(user);
            if(result)
            {
                return Ok();
            }
            return BadRequest("Username already exists or weak password!");
        }

    }
}
