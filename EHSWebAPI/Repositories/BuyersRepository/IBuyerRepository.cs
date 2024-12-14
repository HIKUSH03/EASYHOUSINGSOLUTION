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
        IList<Buyer> GetAllBuyers();
        Buyer GetBuyerById(int id);

        IEnumerable<Property> GetPropertyByPrice();

        //IEnumerable<Cart> AddToCart(int buyerId, int propertyId);

        bool RemoveFromCart(int? id);

    }
}
