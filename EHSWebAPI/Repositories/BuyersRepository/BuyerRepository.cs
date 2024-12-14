using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;

namespace EHSWebAPI.Repositories.BuyersRepository
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly EHSDbContext _eHSDbContext;
        public BuyerRepository(EHSDbContext eHSDbContext)
        {
           _eHSDbContext = eHSDbContext;
        }

        

        public IList<Buyer> GetAllBuyers()
        {
            return _eHSDbContext.Buyers.ToList();
            //throw new NotImplementedException();
        }

        public Buyer GetBuyerById(int id)
        {
            return _eHSDbContext.Buyers.Find(id);
        }

        public IEnumerable<Property> GetPropertyByPrice()
        {
            return _eHSDbContext.Properties.ToList().OrderBy(x => x.PriceRange);
        }

        public IEnumerable<Cart> AddToCart(int buyerId, int propertyId)
        {
            Cart cart = new Cart()
            {
                BuyerId = buyerId,
                PropertyId = propertyId,
                Buyer = _eHSDbContext.Buyers.FirstOrDefault(b => b.BuyerId == buyerId),
                Property = _eHSDbContext.Properties.FirstOrDefault(p => p.PropertyId == propertyId)
            };
            _eHSDbContext.Carts.Add(cart);
            _eHSDbContext.SaveChanges();
            return _eHSDbContext.Carts.ToList();
        }

        public bool RemoveFromCart(int? id)
        {
            if (id == null) return false;
            Cart cart = _eHSDbContext.Carts.FirstOrDefault(x => x.CartId == id);

            if (cart == null) return false;

            _eHSDbContext.Carts.Remove(cart);
            _eHSDbContext.SaveChanges();
            return true;
        }

    }
}