using System.Collections.Generic;
using System.Web.Http;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.BuyersRepository;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/buyers")]
    public class BuyersController : ApiController
    {
        private readonly IBuyerRepository _buyerRepository;
        public BuyersController()
        {
            EHSDbContext context = new EHSDbContext();
            _buyerRepository = new BuyerRepository(context);
        }

        // create new buyer
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateBuyer([FromBody] Buyer buyer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _buyerRepository.CreateBuyer(buyer);
            return Ok(result);
        }

        // Get all buyers
        [HttpGet]
        [Route("")]
        public IEnumerable<Buyer> Get()
        {
            return _buyerRepository.GetAllBuyers();
        }

        // get buyer by Id
        [HttpGet]
        [Route("{id}")]
        public Buyer GetBuyerById(int id)
        {
            return _buyerRepository.GetBuyerById(id);
        }

        // get buyer by username
        [HttpGet]
        [Route("buyer/{username}")]
        public IHttpActionResult GetBuyerByUsername(string username)
        {
            var result = _buyerRepository.GetBuyerByUsername(username);
            if (result == -1)
                return NotFound();
            return Ok(result);
        }

        // get property by Id
        [HttpGet]
        [Route("property/{id}")]
        public Property GetPropertyById(int id)
        {
            return _buyerRepository.GetPropertyById(id);
        }

        // sort property by Price
        [HttpGet]
        [Route("propertybyprice")]
        public IEnumerable<Property> PropertyByPrice()
        {
            return _buyerRepository.GetPropertyByPrice();
        }

        // add property to cart
        [HttpPost]
        [Route("addtocart")]
        public IHttpActionResult AddToCart(int buyerId, [FromBody] Property property)
        {
            var result = _buyerRepository.AddToCart(buyerId, property);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        // get cart by BuyerId
        [HttpGet]
        [Route("{id}/cart")]
        public IHttpActionResult GetCartByBuyerId(int id)
        {
            var result = _buyerRepository.GetCartByBuyerId(id);
            if (result == null)
                return BadRequest("Cart is null for the buyer");
            return Ok(result);
        }



        // remove from cart
        [HttpDelete]
        [Route("removefromcart/{id}")]
        public IHttpActionResult RemoveFromCart(int id)
        {
            var success = _buyerRepository.RemoveFromCart(id);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpPut]
        [Route("updateProfile")]
        public IHttpActionResult UpdateBuyer([FromBody] Buyer buyer)
        {
             var result = _buyerRepository.UpdateBuyer(buyer);
            if (result)
                return Ok("updated");
            return BadRequest("Failed to update Buyer Details!");
        }

    }
}
