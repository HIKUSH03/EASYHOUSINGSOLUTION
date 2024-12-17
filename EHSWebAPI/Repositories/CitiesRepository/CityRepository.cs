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
            try
            {
                return _eHSDbContext.Cities.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all cities.", ex);
            }
        }

        public City GetCityById(int id)
        {
            try
            {
                return _eHSDbContext.Cities.SingleOrDefault(c => c.CityId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the city with ID {id}.", ex);
            }
        }

        public IEnumerable<City> GetCitiesByState(int stateId)
        {
            try
            {
                return _eHSDbContext.Cities.Where(c => c.StateId == stateId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving cities for state ID {stateId}.", ex);
            }
        }

        public void AddCity(City city)
        {
            try
            {
                _eHSDbContext.Cities.Add(city);
                _eHSDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new city.", ex);
            }
        }

        public void UpdateCity(City city)
        {
            try
            {
                var existingCity = _eHSDbContext.Cities.SingleOrDefault(c => c.CityId == city.CityId);
                if (existingCity != null)
                {
                    existingCity.CityName = city.CityName;
                    existingCity.StateId = city.StateId;
                    _eHSDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception($"City with ID {city.CityId} does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the city with ID {city.CityId}.", ex);
            }
        }

        public void DeleteCity(int id)
        {
            try
            {
                var city = _eHSDbContext.Cities.SingleOrDefault(c => c.CityId == id);
                if (city != null)
                {
                    _eHSDbContext.Cities.Remove(city);
                    _eHSDbContext.SaveChanges();
                }
                else
                {
                    throw new Exception($"City with ID {id} does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the city with ID {id}.", ex);
            }
        }
    }
}