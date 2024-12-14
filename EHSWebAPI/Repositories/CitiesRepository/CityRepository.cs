using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHSWebAPI.Repositories.CitiesRepository
{
    public class CityRepository : ICityRepository
    {
        private readonly EHSDbContext _eHSDbContext;

        public CityRepository(EHSDbContext eHSDbContext)
        {
            _eHSDbContext = eHSDbContext;
        }
        public IEnumerable<City> GetAllCities()
        {
            return _eHSDbContext.Cities.ToList();
        }

        public City GetCityById(int id)
        {
            return _eHSDbContext.Cities.SingleOrDefault(c => c.CityId == id);
        }

        public IEnumerable<City> GetCitiesByState(int stateId)
        {
            return _eHSDbContext.Cities.Where(c => c.StateId == stateId).ToList();
        }

        public void AddCity(City city)
        {
            _eHSDbContext.Cities.Add(city);
            _eHSDbContext.SaveChanges();
        }

        public void UpdateCity(City city)
        {
            var existingCity = _eHSDbContext.Cities.SingleOrDefault(c => c.CityId == city.CityId);
            if (existingCity != null)
            {
                existingCity.CityName = city.CityName;
                existingCity.StateId = city.StateId;
                _eHSDbContext.SaveChanges();
            }
        }

        public void DeleteCity(int id)
        {
            var city = _eHSDbContext.Cities.SingleOrDefault(c => c.CityId == id);
            if (city != null)
            {
                _eHSDbContext.Cities.Remove(city);
                _eHSDbContext.SaveChanges();
            }
        }
    }
}