using EHSDataAccessLayer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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

        //// Basic CRUD operations
        //Task<IEnumerable<Property>> GetAllPropertiesAsync();
        //Task<Property> GetPropertyByIdAsync(int propertyId);
        //Task<Property> AddPropertyAsync(Property property);
        //Task<Property> UpdatePropertyAsync(Property property);

        //// Specialized query operations
        //Task<IEnumerable<Property>> GetPropertiesByRegionAsync(string region);
        //Task<IEnumerable<Property>> GetPropertiesByTypeAsync(string propertyType);
        //Task<IEnumerable<Property>> GetPropertiesBySellerAsync(int sellerId);
        //Task<IEnumerable<Property>> GetPropertiesByStatusAsync(bool isActive);
        //Task<IEnumerable<Property>> SearchPropertiesAsync(string region = null, string propertyType = null, decimal? minPrice = null, decimal? maxPrice = null);

        //// Property verification operations
        //Task<bool> VerifyPropertyAsync(int propertyId, bool isVerified);
        //Task<bool> DeactivatePropertyAsync(int propertyId, string reason);
    }
}
