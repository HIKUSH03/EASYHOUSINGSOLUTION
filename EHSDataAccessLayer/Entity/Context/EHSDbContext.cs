using System.Data.Entity;

namespace EHSDataAccessLayer.Entity.Context
{
    public class EHSDbContext : DbContext
    {

        public EHSDbContext()
            : base("name=EHSDbContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Cart> Carts { get; set; }


    }
}