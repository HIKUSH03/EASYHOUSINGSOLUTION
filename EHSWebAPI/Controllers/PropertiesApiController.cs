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
            _repository = new PropertyRepository(context);
        }

        // Basic CRUD Operations

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProperties()
        {
            try
            {
                var properties = _repository.GetAllProperties();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return InternalServerError(new Exception("An error occurred while retrieving all properties.", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetPropertyById(int id)
        {
            try
            {
                var property = _repository.GetPropertyById(id);
                if (property == null)
                {
                    return NotFound();
                }
                return Ok(property);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return InternalServerError(new Exception($"An error occurred while retrieving the property with ID {id}.", ex));
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddProperty([FromBody] Property property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _repository.AddProperty(property);
                _repository.Save();
                return CreatedAtRoute("DefaultApi", new { id = property.PropertyId }, property);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return InternalServerError(new Exception("An error occurred while adding the property.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateProperty(int id, [FromBody] Property property)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != property.PropertyId)
                {
                    return BadRequest("The property ID in the URL does not match the property ID in the request body.");
                }

                _repository.UpdateProperty(property);
                _repository.Save();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return InternalServerError(new Exception($"An error occurred while updating the property with ID {id}.", ex));
            }
        }

        // Specialized Query Operations

        [HttpGet]
        [Route("region")]
        public IHttpActionResult GetPropertiesByRegion(string region)
        {
            try
            {
                var properties = _repository.GetPropertiesByRegion(region);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while retrieving properties in region '{region}'.", ex));
            }
        }

        [HttpGet]
        [Route("type")]
        public IHttpActionResult GetPropertiesByType(string propertyType)
        {
            try
            {
                var properties = _repository.GetPropertiesByType(propertyType);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while retrieving properties of type '{propertyType}'.", ex));
            }
        }

        [HttpGet]
        [Route("seller/{sellerId:int}")]
        public IHttpActionResult GetPropertiesBySeller(int sellerId)
        {
            try
            {
                var properties = _repository.GetPropertiesBySeller(sellerId);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while retrieving properties for seller with ID {sellerId}.", ex));
            }
        }

        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetPropertiesByStatus(bool isActive)
        {
            try
            {
                var properties = _repository.GetPropertiesByStatus(isActive);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while retrieving properties with status IsActive={isActive}.", ex));
            }
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchProperties(string region = null, string propertyType = null, decimal? price = null)
        {
            try
            {
                var properties = _repository.SearchProperties(region, propertyType, price);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("An error occurred while searching for properties.", ex));
            }
        }

        // Property Verification and Status Management

        [HttpPut]
        [Route("{id:int}/verify")]
        public IHttpActionResult VerifyProperty(int id, [FromBody] bool isVerified)
        {
            try
            {
                _repository.VerifyProperty(id, isVerified);
                _repository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while verifying the property with ID {id}.", ex));
            }
        }

        [HttpPut]
        [Route("{id:int}/deactivate")]
        public IHttpActionResult DeactivateProperty(int id, [FromBody] string reason)
        {
            try
            {
                _repository.DeactivateProperty(id, reason);
                _repository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"An error occurred while deactivating the property with ID {id}.", ex));
            }
        }
    }
}
