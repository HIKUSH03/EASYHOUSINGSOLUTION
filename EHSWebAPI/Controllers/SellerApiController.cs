using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.SellersRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/Seller")]
    public class SellerApiController : ApiController
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerApiController()
        {
            // Manually instantiate the dependency here
            EHSDbContext context = new EHSDbContext();
            _sellerRepository = new SellerRepository(context);
        }

        // GET: api/SellerApi
        [Route("")]
        public IHttpActionResult Get()
        {
            var values = _sellerRepository.GetAll();
            return Ok(values);
        }

        // get seller by username
        [HttpGet]
        [Route("seller/{username}")]
        public IHttpActionResult GetSellerByUsername(string username)
        {
            var result = _sellerRepository.GetSellerByUsername(username);
            if (result == -1)
                return NotFound();
            return Ok(result);
        }

        // GET: api/SellerApi/5


        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetSeller(int id)
        {
            var seller = _sellerRepository.GetSellerById(id);
            if (seller == null)
            {
                return NotFound();
            }
            return Ok(seller);
        }

        // POST: api/sellers
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSeller([FromBody] Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSeller = _sellerRepository.CreateSeller(seller);
            return Created(new Uri(Request.RequestUri + "/" + createdSeller.SellerId), createdSeller);
        }

        // PUT: api/sellers/{id}
        [HttpPost]
        [Route("{id}")]
        public IHttpActionResult UpdateSeller(int id, [FromBody] Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedSeller = _sellerRepository.UpdateSeller(id, seller);
            if (updatedSeller == null)
            {
                return NotFound();
            }

            return Ok(updatedSeller);
        }

        // DELETE: api/sellers/{id}
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteSeller(int id)
        {
            var isDeleted = _sellerRepository.DeleteSeller(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
       
    }
}
