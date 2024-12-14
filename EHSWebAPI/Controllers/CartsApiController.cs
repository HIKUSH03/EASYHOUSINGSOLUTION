using System.Web.Http;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.CartsRepository;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/Cart")]
    public class CartsApiController : ApiController
    {
        private readonly ICartRepository _cartRepository;
        public CartsApiController()
        {
            EHSDbContext context = new EHSDbContext();
            _cartRepository = new CartRepository(context);
        }

        [Route("")]
        // GET: api/CartsApi
        public IHttpActionResult Get()
        {
            var CartsList = _cartRepository.GetAllCarts();
            return Ok(CartsList);
        }

        // GET: api/Cart/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var cart = _cartRepository.GetCartById(id);
            if (cart == null)

                return BadRequest();

            else
                return Ok(cart);
        }



        // POST: api/Cart
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateCart([FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _cartRepository.AddCart(cart);
                return Ok("Cart created successfully.");
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
