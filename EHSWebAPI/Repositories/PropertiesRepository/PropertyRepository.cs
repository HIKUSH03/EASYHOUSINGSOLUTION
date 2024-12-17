using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EHSWebAPI.Repositories.PropertiesRepository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly EHSDbContext _context;

        public PropertyRepository(EHSDbContext context)
        {
            _context = context;
        }

        // CRUD Operations

        public IEnumerable<Property> GetAllProperties()
        {
            try
            {
                return _context.Properties.Include(p => p.Seller).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while retrieving all properties.", ex);
            }
        }

        public Property GetPropertyById(int propertyId)
        {
            try
            {
                return _context.Properties.Include(p => p.Seller).FirstOrDefault(p => p.PropertyId == propertyId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the property with ID {propertyId}.", ex);
            }
        }

        public void AddProperty(Property property)
        {
            try
            {
                _context.Properties.Add(property);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the property.", ex);
            }
        }

        public void UpdateProperty(Property property)
        {
            try
            {
                _context.Entry(property).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the property with ID {property.PropertyId}.", ex);
            }
        }

        // Specialized Query Operations

        public IEnumerable<Property> GetPropertiesByRegion(string region)
        {
            try
            {
                return _context.Properties.Where(p => p.Address.Contains(region)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving properties for region '{region}'.", ex);
            }
        }

        public IEnumerable<Property> GetPropertiesByType(string propertyType)
        {
            try
            {
                return _context.Properties.Where(p => p.PropertyType == propertyType).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving properties of type '{propertyType}'.", ex);
            }
        }

        public IEnumerable<Property> GetPropertiesBySeller(int sellerId)
        {
            try
            {
                return _context.Properties.Where(p => p.SellerId == sellerId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving properties for seller with ID {sellerId}.", ex);
            }
        }

        public IEnumerable<Property> GetPropertiesByStatus(bool isActive)
        {
            try
            {
                return _context.Properties.Where(p => p.IsActive == isActive).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving properties with IsActive = {isActive}.", ex);
            }
        }

        public IEnumerable<Property> SearchProperties(string region = null, string propertyType = null, decimal? price = null)
        {
            try
            {
                var query = _context.Properties.AsQueryable();

                if (!string.IsNullOrEmpty(region))
                {
                    query = query.Where(p => p.Address.Contains(region));
                }

                if (!string.IsNullOrEmpty(propertyType))
                {
                    query = query.Where(p => p.PropertyType == propertyType);
                }

                if (price.HasValue)
                {
                    query = query.Where(p => p.PriceRange == price.Value);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while searching for properties.", ex);
            }
        }

        // Property Verification and Status Management

        public void VerifyProperty(int propertyId, bool isVerified)
        {
            try
            {
                var property = _context.Properties.Find(propertyId);
                if (property != null)
                {
                    property.IsActive = isVerified;
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception($"An error occurred while verifying the property with ID {propertyId}.", ex);
            }
        }

        public void DeactivateProperty(int propertyId, string reason)
        {
            try
            {
                var property = _context.Properties.Find(propertyId);
                if (property != null)
                {
                    property.IsActive = false;
                    // Optionally log the reason somewhere
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while deactivating the property with ID {propertyId}.", ex);
            }
        }

        // Save Changes

        

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // log.Error("Error saving changes", ex);
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }

        public bool DeleteProperty(int propertyId)
        {
            try
            {
                var property = _context.Properties.SingleOrDefault(s => s.PropertyId == propertyId);
                if (property == null) return false;

                _context.Properties.Remove(property);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deactivating the property with ID {propertyId}.", ex);
            }
        }
    }
}
