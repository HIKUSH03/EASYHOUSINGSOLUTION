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

    }
}