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
        [Route("registerBuyer")]
        public IHttpActionResult RegisterBuyer([FromBody] RegisterBuyerDto registerBuyerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _authService.Register(registerBuyerDto);
            if (result == "User regsitered successfully")
                return Ok(result);
            return BadRequest(result);
        }

        //Register Seller
        [HttpPost]
        [Route("registerSeller")]
        public IHttpActionResult RegisterSeller([FromBody] RegisterSellerDto registerSellerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _authService.Register(registerSellerDto);
            if (result == "User regsitered successfully")
                return Ok(result);
            return BadRequest(result);
        }

        //Login User
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (result, userType, userName) = _authService.Authenticate(loginDto);
            if (result == "Authentication successful")
            {
                //Return both UserType And UserName in the response object
                return Ok(new { Message = result, UserType = userType, UserName = userName });
            }
            return BadRequest(result);
        }



    }
}

