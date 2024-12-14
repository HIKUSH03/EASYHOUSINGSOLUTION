using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using EHSWebAPI.Repositories.StatesRepository;

namespace EHSWebAPI.Controllers
{
    [RoutePrefix("api/state")]
    public class StateController : ApiController
    {
        // GET: api/StateApi
        
        private readonly IStateRepository _stateRepository;
        public StateController()
        {
            EHSDbContext context = new EHSDbContext();
            _stateRepository = new StateRepository(context);
        }
        [HttpGet]
        [Route("")]

        public IHttpActionResult GetAllStates()
        {
            var state = _stateRepository.GetAllStates();
            return Ok(state);
        }

        // GET: api/StateApi/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetStateById(int id)
        {
            var state = _stateRepository.GetStateById(id);
            if (state == null)
                return NotFound();

            return Ok(state);
        }

        // POST: api/StateApi
        [HttpPost]
        [Route(" ")]
        public IHttpActionResult AddState([FromBody] State state)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _stateRepository.AddState(state);
            return Created($"api/state/{state.StateId}", state);
        }
       

        // PUT: api/StateApi/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateState(int id, [FromBody] State state)
        {
            state.StateId = id;
            _stateRepository.UpdateState(state);
            return Ok(state);
        }

        // DELETE: api/StateApi/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteState(int id)
        {
            _stateRepository.DeleteState(id);
            return Ok();
        }
    }
}
