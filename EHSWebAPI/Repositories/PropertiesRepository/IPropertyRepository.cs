using EHSDataAccessLayer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace EHSWebAPI.Repositories.PropertiesRepository
{
    public interface IPropertyRepository
    {
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
