using EHSDataAccessLayer.Entity;
using System.Collections.Generic;

namespace EHSWebAPI.Repositories.StatesRepository
{
    public interface IStateRepository
    {
        IEnumerable<State> GetAllStates();
        State GetStateById(int id);
        void AddState(State state);
        void UpdateState(State state);
        void DeleteState(int id);
    }
}
