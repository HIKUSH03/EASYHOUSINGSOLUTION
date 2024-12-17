using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        }

        public int GetBuyerByUsername(string username)
        {
            var response = _eHSDbContext.Buyers.FirstOrDefault(x => x.UserName == username).BuyerId;
            if(response == 0)
            {
                return -1;
            }
            return response;
        }

        public Buyer CreateBuyer(Buyer buyer)
        {

            _eHSDbContext.Buyers.Add(buyer);
            _eHSDbContext.SaveChanges();
            return buyer;
        }

        public Property GetPropertyById(int id)
        {
            return _eHSDbContext.Properties.Find(id);
        }
        public Buyer GetBuyerById(int id)
        {
            return _eHSDbContext.Buyers.Find(id);
        }

        public IEnumerable<Property> GetPropertyByPrice()
        {
            return _eHSDbContext.Properties.ToList().OrderBy(x => x.PriceRange);
        }

        // Add property to cart
        public bool AddToCart(int buyerId, Property property)
        {
            // check if buyer already has a cart
            var cartExists = _eHSDbContext.Carts.Find(buyerId);

            if (cartExists == null)
            {
                // buyer does not has a cart, create new and add in it
                Cart cart = new Cart
                {
                    BuyerId = buyerId,
                    Properties = new List<Property> { property }
                };
                _eHSDbContext.Carts.Add(cart);
                _eHSDbContext.SaveChanges();
                return true;
            }
            // buyer has a cart, add property to it
            cartExists.Properties.Add(property);
            _eHSDbContext.SaveChanges();
            return true;
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