using EHSDataAccessLayer.Entity;
using System.Collections;
using System.Collections.Generic;
namespace EHSWebAPI.Repositories.SellersRepository
{
    public interface ISellerRepository
    {
        IEnumerable<Seller> GetAll();

        Seller GetSellerById(int id);
        Seller CreateSeller(Seller seller);
        Seller UpdateSeller(int id, Seller seller);
        bool DeleteSeller(int id);
        Property AddPropertyToSeller(int sellerId, Property property);
        IEnumerable<Property> GetPropertiesBySeller(int sellerId);

    }
}
