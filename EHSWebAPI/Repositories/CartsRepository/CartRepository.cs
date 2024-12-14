using System;
using System.Collections.Generic;
using System.Linq;
using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;

namespace EHSWebAPI.Repositories.CartsRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly EHSDbContext _context;


        public CartRepository(EHSDbContext context)
        {
            _context = context;
        }



        public IEnumerable<Cart> GetAllCarts()
        {
            return _context.Carts.ToList();
        }


        public Cart GetCartById(int cartId)
        {
            return _context.Carts.Find(cartId);
        }


        public void DeleteCart(int cartId)
        {
            var cart = _context.Carts.Find(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }

        }


        public void UpdateCart(Cart cart)
        {

            throw new NotImplementedException();
        }
    }
}