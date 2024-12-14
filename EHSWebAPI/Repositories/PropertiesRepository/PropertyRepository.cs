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
        //private readonly EHSDbContext _context;

        //public PropertyRepository(EHSDbContext context)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}

        //public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        //{
        //    // Return all active properties with their related seller information
        //    return await _context.Properties
        //        .Include(p => p.Seller)
        //        .Where(p => p.IsActive)
        //        .OrderBy(p => p.PropertyName)
        //        .ToListAsync();
        //}

        //public async Task<Property> GetPropertyByIdAsync(int propertyId)
        //{
        //    // Return a specific property with its related seller information
        //    return await _context.Properties
        //        .Include(p => p.Seller)
        //        .FirstOrDefaultAsync(p => p.PropertyId == propertyId);
        //}

        //public async Task<Property> AddPropertyAsync(Property property)
        //{
        //    // Validate the property data
        //    if (property == null)
        //        throw new ArgumentNullException(nameof(property));

        //    // Set default values for new properties
        //    property.IsActive = false; // New properties require verification

        //    // Add the property to the context
        //    _context.Properties.Add(property);
        //    await _context.SaveChangesAsync();

        //    return property;
        //}

        //public async Task<Property> UpdatePropertyAsync(Property property)
        //{
        //    // Validate the property data
        //    if (property == null)
        //        throw new ArgumentNullException(nameof(property));

        //    // Get the existing property
        //    var existingProperty = await _context.Properties
        //        .FirstOrDefaultAsync(p => p.PropertyId == property.PropertyId);

        //    if (existingProperty == null)
        //        throw new InvalidOperationException($"Property with ID {property.PropertyId} not found");

        //    // Update the existing property's fields
        //    existingProperty.PropertyName = property.PropertyName;
        //    existingProperty.PropertyType = property.PropertyType;
        //    existingProperty.PropertyOption = property.PropertyOption;
        //    existingProperty.Description = property.Description;
        //    existingProperty.Address = property.Address;
        //    existingProperty.PriceRange = property.PriceRange;
        //    existingProperty.InitialDeposit = property.InitialDeposit;
        //    existingProperty.Landmark = property.Landmark;

        //    // Save changes
        //    await _context.SaveChangesAsync();

        //    return existingProperty;
        //}

        //public async Task<IEnumerable<Property>> GetPropertiesByRegionAsync(string region)
        //{
        //    return await _context.Properties
        //        .Include(p => p.Seller)
        //        .Where(p => p.IsActive && p.Address.Contains(region))
        //        .OrderBy(p => p.PropertyName)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Property>> GetPropertiesByTypeAsync(string propertyType)
        //{
        //    return await _context.Properties
        //        .Include(p => p.Seller)
        //        .Where(p => p.IsActive && p.PropertyType == propertyType)
        //        .OrderBy(p => p.PropertyName)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Property>> GetPropertiesBySellerAsync(int sellerId)
        //{
        //    return await _context.Properties
        //        .Include(p => p.Seller)
        //        .Where(p => p.SellerId == sellerId)
        //        .OrderBy(p => p.PropertyName)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Property>> GetPropertiesByStatusAsync(bool isActive)
        //{
        //    return await _context.Properties
        //        .Include(p => p.Seller)
        //        .Where(p => p.IsActive == isActive)
        //        .OrderBy(p => p.PropertyName)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Property>> SearchPropertiesAsync(
        //    string region = null,
        //    string propertyType = null,
        //    decimal? minPrice = null,
        //    decimal? maxPrice = null)
        //{
        //    // Start with a base query
        //    var query = _context.Properties
        //        .Include(p => p.Seller)
        //        .Where(p => p.IsActive);

        //    // Apply filters if provided
        //    if (!string.IsNullOrWhiteSpace(region))
        //        query = query.Where(p => p.Address.Contains(region));

        //    if (!string.IsNullOrWhiteSpace(propertyType))
        //        query = query.Where(p => p.PropertyType == propertyType);

        //    if (minPrice.HasValue)
        //        query = query.Where(p => p.PriceRange >= minPrice.Value);

        //    if (maxPrice.HasValue)
        //        query = query.Where(p => p.PriceRange <= maxPrice.Value);

        //    // Return the filtered results
        //    return await query
        //        .OrderBy(p => p.PropertyName)
        //        .ToListAsync();
        //}

        //public async Task<bool> VerifyPropertyAsync(int propertyId, bool isVerified)
        //{
        //    var property = await _context.Properties
        //        .FirstOrDefaultAsync(p => p.PropertyId == propertyId);

        //    if (property == null)
        //        return false;

        //    property.IsActive = isVerified;
        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        //public async Task<bool> DeactivatePropertyAsync(int propertyId, string reason)
        //{
        //    var property = await _context.Properties
        //        .FirstOrDefaultAsync(p => p.PropertyId == propertyId);

        //    if (property == null)
        //        return false;

        //    property.IsActive = false;
        //    // You might want to add a Reason field to the Property model to store this
        //    await _context.SaveChangesAsync();

        //    return true;
        //}

    }
}