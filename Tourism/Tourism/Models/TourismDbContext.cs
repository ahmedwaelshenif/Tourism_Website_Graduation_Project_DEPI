using Microsoft.EntityFrameworkCore;
using Tourism.Models;
using Tourism.Models.Relations;

namespace Tourism.Models
{
    public class TourismDbContext : DbContext
    {
        public TourismDbContext(DbContextOptions<TourismDbContext> options)
            : base(options) { }

        // ====== DbSets ======
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Tourist> Tourists { get; set; }
        public DbSet<TourGuide> TourGuides { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        // ====== Relations ======
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<FavouriteProduct> FavouriteProducts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<InboxMsg> InboxMsgs { get; set; }
        public DbSet<MerchantSocialMedia> MerchantSocialMedias { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<TouristFeedback> TouristFeedbacks { get; set; }
        public DbSet<TouristProduct> TouristProducts { get; set; }
        public DbSet<TouristRestaurant> TouristRestaurants { get; set; }
        public DbSet<TouristRoom> TouristRooms { get; set; }
        public DbSet<TouristTrip> TouristTrips { get; set; }
        public DbSet<VerificationRequest> VerificationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-many composite keys
            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.TouristId, cp.ProductId });

            modelBuilder.Entity<FavouriteProduct>()
                .HasKey(fp => new { fp.touristId, fp.productId });

            // Merchant ↔ SocialMedia
            modelBuilder.Entity<MerchantSocialMedia>()
                .HasOne(m => m.Merchant)
                .WithMany(s => s.SocialMediaLinks)
                .HasForeignKey(m => m.MerchantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product ↔ Merchant
            modelBuilder.Entity<Product>()
                .HasOne(p => p.merchant)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.merchantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hotel ↔ Room
            modelBuilder.Entity<Room>()
                .HasOne(r => r.hotel)
                .WithMany(h => h.rooms)
                .HasForeignKey(r => r.hotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Restaurant ↔ Meal
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.restaurant)
                .WithMany(r => r.meals)
                .HasForeignKey(m => m.restaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Trip ↔ TourGuide
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.tourGuide)
                .WithMany(g => g.trips)
                .HasForeignKey(t => t.tourGuideId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
