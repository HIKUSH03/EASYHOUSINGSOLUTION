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

    }
}
