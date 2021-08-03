using System;

namespace BookChallenge.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime? Cancelation { get; set; }
    }
}
