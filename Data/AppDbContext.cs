using BillManager.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BillManager.Data
{
    public class AppDbContext : IdentityDbContext<Company>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillEntry> BillEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<BillEntry>()
                .HasOne(be => be.Bill)
                .WithMany(b => b.BillEntries)
                .HasForeignKey(be => be.BillId);

            builder.Entity<BillEntry>()
                .HasOne(be => be.Product)
                .WithMany(p => p.BillEntries)
                .HasForeignKey(be => be.ProductId);

            builder.Entity<Bill>()
                .HasOne(b => b.SellerCompany)
                .WithMany(c => c.SellerBills)
                .HasForeignKey(b => b.SellerCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bill>()
                .HasOne(b => b.BuyerCompany)
                .WithMany(c => c.BuyerBills)
                .HasForeignKey(b => b.BuyerCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bill>()
                .Property(b => b.BillId)
                .ValueGeneratedOnAdd();

            builder.Entity<Product>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<BillEntry>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

        }

    }
}
