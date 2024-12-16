using System.Web.Http;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.DTOs;
using EHSWebAPI.Services;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly AuthServices _authService;
        public AuthController()
        {
            EHSDbContext context = new EHSDbContext();
            _authService = new AuthServices(context);
        }

        //Register user
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _authService.Register(registerDto);
            if (result == "User regsitered successfully")
                return Ok(result);
            return BadRequest(result);
        }

        //Login User
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginDto loginDto)
        {
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var (result, userType) = _authService.Authenticate(loginDto);
                if (result == "Authenticate successful")
                {


                    return Ok((result));
                }
                return BadRequest(result);
            }
        }


    }
}

