using BookChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace BookChallenge
{
    public class BookingChallengeContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public BookingChallengeContext(DbContextOptions<BookingChallengeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasKey(h => h.Id);
            modelBuilder.Entity<Booking>().Property(p => p.Id).IsRequired();
            modelBuilder.Entity<Booking>().Property(p => p.Start).IsRequired();
            modelBuilder.Entity<Booking>().Property(p => p.End).IsRequired();
            modelBuilder.Entity<Booking>().Property(p => p.CustomerName).IsRequired();
            modelBuilder.Entity<Booking>().Property(p => p.Cancelation);
        }
    }
}
