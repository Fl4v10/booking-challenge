using BookChallenge.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookChallenge
{
    public interface IDeleteBookingRepository {
        Booking Delete(int id);
    }

    public interface ISaveBookingRepository {
        Task<Booking> Save(Booking booking);
        Task<Booking> Save(int id, Booking booking);
    }

    public interface IGetBookingRepository
    {
        Task<Booking> Get(int id);
        bool CheckRoomAvaibility(DateTime from, DateTime to);
    }

    public interface IBookingRepository : IGetBookingRepository, ISaveBookingRepository, IDeleteBookingRepository
    {
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly BookingChallengeContext _context;

        public BookingRepository(BookingChallengeContext context)
        {
            _context = context;
        }

        public Booking Delete(int id)
        {
            var item = _context.Bookings
                .SingleOrDefault(s => s.Id == id);

            if (item == null)
                return null;

            item.Cancelation = DateTime.UtcNow;

            _context.Update(item);
            _context.SaveChanges();

            return item;
        }

        public async Task<Booking> Get(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public bool CheckRoomAvaibility(DateTime from, DateTime to)
        {
            return _context.Bookings.Any(x => x.Start.Date >= from.Date && x.End.Date <= to.Date);
        }

        public async Task<Booking> Save(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking> Save(int id, Booking booking)
        {
            var _booking = await _context.Bookings.FindAsync(id);
            _booking.CustomerName = booking.CustomerName;
            _booking.Start = booking.Start;
            _booking.End = booking.End;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }
    }
}
