using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.CitiesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EHSWebAPI.Controllers
{
    public class CityApiController : ApiController
    {
        // GET: api/CityApi
        [RoutePrefix("api/city")]
        public class CityController : ApiController
        {
            private readonly ICityRepository _cityRepository;

            public CityController()
            {
                EHSDbContext context = new EHSDbContext();
                _cityRepository = new CityRepository(context);
            }

            // GET: api/city
            [HttpGet]
            [Route("")]
            public IHttpActionResult GetAllCities()
            {
                try
                {
                    var cities = _cityRepository.GetAllCities();
                    return Ok(cities);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving all cities.", ex);
                }
            }

            // GET: api/city/{id}
            [HttpGet]
            [Route("{id:int}")]
            public IHttpActionResult GetCityById(int id)
            {
                try
                {
                    var city = _cityRepository.GetCityById(id);
                    if (city == null)
                        return NotFound();

                    return Ok(city);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while retrieving the city with ID {id}.", ex);
                }
            }

            // GET: api/city/state/{stateId}
            [HttpGet]
            [Route("state/{stateId:int}")]
            public IHttpActionResult GetCitiesByState(int stateId)
            {
                try
                {
                    var cities = _cityRepository.GetCitiesByState(stateId);
                    return Ok(cities);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while retrieving cities for state ID {stateId}.", ex);
                }
            }

            // POST: api/city
            [HttpPost]
            [Route("")]
            public IHttpActionResult AddCity([FromBody] City city)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    _cityRepository.AddCity(city);
                    return Created($"api/city/{city.CityId}", city);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding a new city.", ex);
                }
            }

            // PUT: api/city/{id}
            [HttpPut]
            [Route("{id:int}")]
            public IHttpActionResult UpdateCity(int id, [FromBody] City city)
            {
                try
                {
                    city.CityId = id;
                    _cityRepository.UpdateCity(city);
                    return Ok(city);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while updating the city with ID {id}.", ex);
                }
            }

            // DELETE: api/city/{id}
            [HttpDelete]
            [Route("{id:int}")]
            public IHttpActionResult DeleteCity(int id)
            {
                try
                {
                    _cityRepository.DeleteCity(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while deleting the city with ID {id}.", ex);
                }
            }
        }
    }
}