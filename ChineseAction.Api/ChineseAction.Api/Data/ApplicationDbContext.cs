using ChineseAction.Api.NewFolder;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChineseAction.Api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        // הגדרת הטבלאות (DbSets) לפי האיפיון
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Purchaser> Purchasers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Winner> Winners { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. קשר בין תורם למתנה (One-to-Many)
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Donor)
                .WithMany(d => d.Gifts)
                .HasForeignKey(g => g.DonorId)
              .OnDelete(DeleteBehavior.Cascade);

            // 2. קשר בין מתנה לזוכה (One-to-One) - הכי מקצועי
            // ה-GiftId נמצא בטבלת Winners
            modelBuilder.Entity<Winner>()
            .HasOne(w => w.Gift)
            .WithOne(g => g.Winner)
           .HasForeignKey<Winner>(w => w.GiftId);

            // 3. הגדרת מחיר הכרטיס כ-Decimal עם דיוק מתאים
            modelBuilder.Entity<Gift>()
            .Property(g => g.TicketPrice)
            .HasPrecision(18, 2);

            // 4. קשר בין הזמנה לפריטי הזמנה (One-to-Many)
            modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

            // 5. קשר בין רוכש להזמנות שלו
            modelBuilder.Entity<Order>()
            .HasOne(o => o.Purchaser)
            .WithMany(p => p.Orders)
           .HasForeignKey(o => o.PurchaserId);
            
        base.OnModelCreating(modelBuilder);
        }
    }
}
