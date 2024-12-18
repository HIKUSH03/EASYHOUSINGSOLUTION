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
        int GetBuyerByUsername(string username);
        IList<Buyer> GetAllBuyers();
        //bool UpdateBuyer(Buyer buyer);
        Property GetPropertyById(int id);
        IEnumerable<Property> GetPropertyByPrice();
        Cart GetCartByBuyerId(int buyerId);
        bool AddToCart(int buyerId, Property property);
        bool RemoveFromCart(int? id);

    }
}
