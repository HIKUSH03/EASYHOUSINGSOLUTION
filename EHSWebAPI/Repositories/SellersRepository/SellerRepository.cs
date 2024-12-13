using EHSDataAccessLayer.Entity;
using EHSDataAccessLayer.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHSWebAPI.Repositories.SellersRepository
{
    public class SellerRepository : ISellerRepository
    {
        private readonly EHSDbContext _eHSDbContext;

        public SellerRepository(EHSDbContext eHSDbContext)
        {
            _eHSDbContext = eHSDbContext;
        }
        public IEnumerable<Seller> GetAll()
        {
            return _eHSDbContext.Sellers.ToList();
        }
    }
}