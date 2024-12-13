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

        // GET: api/SellerApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SellerApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SellerApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SellerApi/5
        public void Delete(int id)
        {
        }
    }
}
