using EHSDataAccessLayer.Entity;
using System.Collections;
using System.Collections.Generic;
namespace EHSWebAPI.Repositories.SellersRepository
{
    public interface ISellerRepository
    {
        IEnumerable<Seller> GetAll();
        
    }
}
