using System.Collections.Generic;
using EHSDataAccessLayer.Entity;

namespace EHSWebAPI.Repositories.CartsRepository
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetAllCarts();
        Cart GetCartById(int cartId);
        void UpdateCart(Cart cart);
        void DeleteCart(int cartId);
        void AddCart(Cart cart);
    }
}
