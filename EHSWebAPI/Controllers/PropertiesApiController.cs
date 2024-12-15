using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.CitiesRepository;
using EHSWebAPI.Repositories.PropertiesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EHSWebAPI.Controllers
{
    public class PropertiesApiController : ApiController
    {
        private readonly IPropertyRepository _repository;

        public PropertiesApiController()
        {
            EHSDbContext context = new EHSDbContext();
            _repository=new PropertyRepository(context);
            
        }
        

        // Basic CRUD Operations

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProperties()
        {
            var properties = _repository.GetAllProperties();
            return Ok(properties);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetPropertyById(int id)
        {
            var property = _repository.GetPropertyById(id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddProperty([FromBody] Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.AddProperty(property);
            _repository.Save();
            return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateProperty(int id, [FromBody] Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                return BadRequest();
            }

            _repository.UpdateProperty(property);
            _repository.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // Specialized Query Operations

        [HttpGet]
        [Route("region")]
        public IHttpActionResult GetPropertiesByRegion(string region)
        {
            var properties = _repository.GetPropertiesByRegion(region);
            return Ok(properties);
        }

        [HttpGet]
        [Route("type")]
        public IHttpActionResult GetPropertiesByType(string propertyType)
        {
            var properties = _repository.GetPropertiesByType(propertyType);
            return Ok(properties);
        }

        [HttpGet]
        [Route("seller/{sellerId:int}")]
        public IHttpActionResult GetPropertiesBySeller(int sellerId)
        {
            var properties = _repository.GetPropertiesBySeller(sellerId);
            return Ok(properties);
        }

        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetPropertiesByStatus(bool isActive)
        {
            var properties = _repository.GetPropertiesByStatus(isActive);
            return Ok(properties);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchProperties(string region = null, string propertyType = null, decimal? price = null)
        {
            var properties = _repository.SearchProperties(region, propertyType, price);
            return Ok(properties);
        }

        // Property Verification and Status Management

        [HttpPut]
        [Route("{id:int}/verify")]
        public IHttpActionResult VerifyProperty(int id, [FromBody] bool isVerified)
        {
            _repository.VerifyProperty(id, isVerified);
            _repository.Save();
            return Ok();
        }

        [HttpPut]
        [Route("{id:int}/deactivate")]
        public IHttpActionResult DeactivateProperty(int id, [FromBody] string reason)
        {
            _repository.DeactivateProperty(id, reason);
            _repository.Save();
            return Ok();
        }
    }
}
