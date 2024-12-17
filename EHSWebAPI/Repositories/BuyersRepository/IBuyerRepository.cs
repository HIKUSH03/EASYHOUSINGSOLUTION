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
        //bool UpdateBuyer(Buyer buyer);
        Property GetPropertyById(int id);
        IEnumerable<Property> GetPropertyByPrice();
        bool AddToCart(int buyerId, Property property);
        bool RemoveFromCart(int? id);

    }
}
