using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EHSWebAPI.Repositories.PropertiesRepository
{
    public class PropertyRepository:IPropertyRepository
    {
        private readonly EHSDbContext _context;

        public PropertyRepository(EHSDbContext context)
        {
            _context = context;
        }

        // CRUD Operations

        public IEnumerable<Property> GetAllProperties()
        {
            return _context.Properties.Include(p => p.Seller).ToList();
        }

        public Property GetPropertyById(int propertyId)
        {
            return _context.Properties.Include(p => p.Seller).FirstOrDefault(p => p.PropertyId == propertyId);
        }

        public void AddProperty(Property property)
        {
            _context.Properties.Add(property);
        }

        public void UpdateProperty(Property property)
        {
            _context.Entry(property).State = EntityState.Modified;
        }

        // Specialized Query Operations

        public IEnumerable<Property> GetPropertiesByRegion(string region)
        {
            return _context.Properties.Where(p => p.Address.Contains(region)).ToList();
        }

        public IEnumerable<Property> GetPropertiesByType(string propertyType)
        {
            return _context.Properties.Where(p => p.PropertyType == propertyType).ToList();
        }

        public IEnumerable<Property> GetPropertiesBySeller(int sellerId)
        {
            return _context.Properties.Where(p => p.SellerId == sellerId).ToList();
        }

        public IEnumerable<Property> GetPropertiesByStatus(bool isActive)
        {
            return _context.Properties.Where(p => p.IsActive == isActive).ToList();
        }

        public IEnumerable<Property> SearchProperties(string region = null, string propertyType = null, decimal? price = null)
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


        // Property Verification and Status Management

        public void VerifyProperty(int propertyId, bool isVerified)
        {
            var property = _context.Properties.Find(propertyId);
            if (property != null)
            {
                property.IsActive = isVerified;
            }
        }

        public void DeactivateProperty(int propertyId, string reason)
        {
            var property = _context.Properties.Find(propertyId);
            if (property != null)
            {
                property.IsActive = false;
                // Optionally log the reason somewhere
            }
        }

        // Save Changes

        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}