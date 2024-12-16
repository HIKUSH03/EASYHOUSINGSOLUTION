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
        public Seller GetSellerById(int id)
        {
            return _eHSDbContext.Sellers.SingleOrDefault(s => s.SellerId == id);
        }

        public Seller CreateSeller(Seller seller)
        {
            _eHSDbContext.Sellers.Add(seller);
            _eHSDbContext.SaveChanges();
            return seller;
        }

        public Seller UpdateSeller(int id, Seller seller)
        {
            var existingSeller = _eHSDbContext.Sellers.SingleOrDefault(s => s.SellerId == id);
            if (existingSeller == null) return null;

            existingSeller.FirstName = seller.FirstName;
            existingSeller.LastName = seller.LastName;
            existingSeller.PhoneNo = seller.PhoneNo;
            existingSeller.Address = seller.Address;
            existingSeller.StateId = seller.StateId;
            existingSeller.CityId = seller.CityId;
            existingSeller.EmailId = seller.EmailId;

            _eHSDbContext.SaveChanges();
            return existingSeller;
        }

        public bool DeleteSeller(int id)
        {
            var seller = _eHSDbContext.Sellers.SingleOrDefault(s => s.SellerId == id);
            if (seller == null) return false;

            _eHSDbContext.Sellers.Remove(seller);
            _eHSDbContext.SaveChanges();
            return true;
        }

      
    }
}