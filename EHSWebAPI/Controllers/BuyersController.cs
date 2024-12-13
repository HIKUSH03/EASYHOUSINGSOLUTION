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

        // GET: api/Buyers/5
        public Buyer Get(int id)
        {
            return _buyerRepository.GetBuyerById(id);
        }

        // POST: api/Buyers
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Buyers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Buyers/5
        public void Delete(int id)
        {
        }
    }
}
