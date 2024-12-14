using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.UsersRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersApiController : ApiController
    {
        
        
        private readonly IUserRepository _userRepository;

        public UsersApiController()
        {
            EHSDbContext context = new EHSDbContext();
            _userRepository = new UserRepository(context);
        }
        
        // GET: api/UsersApi

        [Route("")]
        public IHttpActionResult GetAllUser()
        {
            var values = _userRepository.GetAllUser();
            return Ok(values);
        }

        // GET: api/UsersApi/5
        [HttpGet]
        [Route("{UserName}")]
        public IHttpActionResult GetUserByName(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/UsersApi
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = _userRepository.CreateUser(user);
            return Created(new Uri(Request.RequestUri + "/" + createdUser.UserName), createdUser);
        }

        // PUT: api/UsersApi/5
       

        // DELETE: api/UsersApi/5
        [HttpDelete]
        [Route("{UserName}")]
        public IHttpActionResult DeleteSeller(string userName)
        {
            var isDeleted = _userRepository.DeleteUser(userName);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
