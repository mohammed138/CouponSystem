

using CouponSystem.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CouponSystem.DataAccess.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponProduct> CouponProducts { get; set; }
        public DbSet<CouponUser> CouponUsers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}