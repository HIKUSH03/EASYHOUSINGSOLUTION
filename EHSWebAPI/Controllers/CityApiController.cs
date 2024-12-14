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
                _cityRepository = new  CityRepository(context);
            }

            // GET: api/city
            [HttpGet]
            [Route("")]
            public IHttpActionResult GetAllCities()
            {
                var cities = _cityRepository.GetAllCities();
                return Ok(cities);
            }

            // GET: api/city/{id}
            [HttpGet]
            [Route("{id:int}")]
            public IHttpActionResult GetCityById(int id)
            {
                var city = _cityRepository.GetCityById(id);
                if (city == null)
                    return NotFound();

                return Ok(city);
            }

            // GET: api/city/state/{stateId}
            [HttpGet]
            [Route("state/{stateId:int}")]
            public IHttpActionResult GetCitiesByState(int stateId)
            {
                var cities = _cityRepository.GetCitiesByState(stateId);
                return Ok(cities);
            }

            // POST: api/city
            [HttpPost]
            [Route("")]
            public IHttpActionResult AddCity([FromBody] City city)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _cityRepository.AddCity(city);
                return Created($"api/city/{city.CityId}", city);
            }

            // PUT: api/city/{id}
            [HttpPut]
            [Route("{id:int}")]
            public IHttpActionResult UpdateCity(int id, [FromBody] City city)
            {
                city.CityId = id;
                _cityRepository.UpdateCity(city);
                return Ok(city);
            }

            // DELETE: api/city/{id}
            [HttpDelete]
            [Route("{id:int}")]
            public IHttpActionResult DeleteCity(int id)
            {
                _cityRepository.DeleteCity(id);
                return Ok();
            }
        }
    }
}
