using EHSDataAccessLayer.Entity;
using System.Collections.Generic;

namespace EHSWebAPI.Repositories.PropertiesRepository
{
    public interface IPropertyRepository
    {
        IEnumerable<Property> GetAllProperties();
        Property GetPropertyById(int propertyId);
        void AddProperty(Property property);
        void UpdateProperty(Property property);

        // Specialized Query Operations
        IEnumerable<Property> GetPropertiesByRegion(string region);
        IEnumerable<Property> GetPropertiesByType(string propertyType);
        IEnumerable<Property> GetPropertiesBySeller(int sellerId);
        IEnumerable<Property> GetPropertiesByStatus(bool isActive);
        IEnumerable<Property> SearchProperties(string region = null, string propertyType = null, decimal? price = null);

        // Property Verification and Status Management
        void VerifyProperty(int propertyId, bool isVerified);
        void DeactivateProperty(int propertyId, string reason);

        // Save Changes
        void Save();
    }
}
