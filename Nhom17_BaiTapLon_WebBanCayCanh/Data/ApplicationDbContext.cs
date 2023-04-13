using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nhom17_BaiTapLon_WebBanCayCanh.Models;

namespace Nhom17_BaiTapLon_WebBanCayCanh.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductOption> ProductOptions { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Shipping> Shipping { get; set; }
        //public DbSet<Payment> Payment { get; set; }
    }
}
