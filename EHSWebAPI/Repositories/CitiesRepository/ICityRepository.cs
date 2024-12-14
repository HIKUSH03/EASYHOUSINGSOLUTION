using EHSDataAccessLayer.Entity;
using System.Collections.Generic;

namespace EHSWebAPI.Repositories.CitiesRepository
{
    public interface ICityRepository
    {
        IEnumerable<City> GetAllCities();
        City GetCityById(int id);
        IEnumerable<City> GetCitiesByState(int stateId);
        void AddCity(City city);
        void UpdateCity(City city);
        void DeleteCity(int id);
    }
}
