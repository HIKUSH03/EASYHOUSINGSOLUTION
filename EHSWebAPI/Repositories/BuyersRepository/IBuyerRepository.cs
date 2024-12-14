using System;
using System.Collections;
using EHSDataAccessLayer.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHSWebAPI.Repositories.BuyersRepository
{
    public interface IBuyerRepository
    {
        Buyer CreateBuyer(Buyer buyer);
        IList<Buyer> GetAllBuyers();
        Property GetPropertyById(int id);
        IEnumerable<Property> GetPropertyByPrice();

        IEnumerable<Cart> AddToCart(int buyerId, int propertyId);

        bool RemoveFromCart(int? id);

    }
}
