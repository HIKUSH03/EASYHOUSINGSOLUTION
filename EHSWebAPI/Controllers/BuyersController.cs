using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        // GET: api/Buyers
        [HttpGet]
        [Route("")]
        public IEnumerable<Buyer> Get()
        {
            return _buyerRepository.GetAllBuyers();
        }

        [HttpGet]
        [Route("propertybyprice")]
        public IEnumerable<Property> PropertyByPrice()
        {
            return _buyerRepository.GetPropertyByPrice();
        }


        [HttpPost]
        [Route("addtocart/{buyerId}/{propertyId}")]
        public IHttpActionResult AddToCart(int buyerId, int propertyId)
        {

            var result = _buyerRepository.AddToCart(buyerId, propertyId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("removefromcart/{id}")]
        public IHttpActionResult RemoveFromCart(int id)
        {
            var success = _buyerRepository.RemoveFromCart(id);
            if(success)
            {
                return Ok();
            }
            return NotFound();
        }

    }
}
