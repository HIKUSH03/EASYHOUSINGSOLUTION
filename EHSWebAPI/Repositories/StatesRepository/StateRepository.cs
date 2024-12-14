using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHSWebAPI.Repositories.StatesRepository
{
    public class StateRepository : IStateRepository
    {
        private readonly EHSDbContext _eHSDbContext;
        public StateRepository(EHSDbContext eHSDbContext)
        {
            _eHSDbContext = eHSDbContext;
        }
        public IEnumerable<State> GetAllStates()
        {
            return _eHSDbContext.States.ToList();
        }
        public State GetStateById(int id)
        {
            return _eHSDbContext.States.SingleOrDefault(s => s.StateId == id);
        }
        public void AddState(State state)
        {
            _eHSDbContext.States.Add(state);
            _eHSDbContext.SaveChanges();
        }

        public void DeleteState(int id)
        {
            var state = _eHSDbContext.States.SingleOrDefault(s => s.StateId == id);
            if (state!=null)
            {
                var toRemove = _eHSDbContext.Sellers.FirstOrDefault(x => x.StateId == id);
                var toRemoveCity = _eHSDbContext.Cities.FirstOrDefault(x => x.StateId==id);

                _eHSDbContext.Cities.Remove(toRemoveCity);
                _eHSDbContext.Sellers.Remove(toRemove);
                _eHSDbContext.States.Remove(state);
                _eHSDbContext.SaveChanges();
            }
        }
     
        public void UpdateState(State state)
        {
            var existState = _eHSDbContext.States.SingleOrDefault(s => s.StateId == state.StateId);
            if(existState!=null)
            {
                existState.StateName = state.StateName;
                _eHSDbContext.SaveChanges();
            }
        }
    }
}