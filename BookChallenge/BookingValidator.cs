using BookChallenge.Models;
using BookChallenge.Utils;
using FluentValidation;
using System;

namespace BookChallenge
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.CustomerName).NotNull().NotEmpty();

            RuleFor(b => b.Start).NotNull().NotEmpty();
            RuleFor(b => b.End).NotNull().NotEmpty();

            RuleFor(b => b.Start)
                .Must(m => m.Date.IsGreaterThanOrEqual(DateTime.Today.AddDays(1)))
                .WithMessage("All reservations start at least the next day of booking!");

            RuleFor(b => b.Start)
                 .Must(m => !m.Date.IsGreaterThanOrEqual(DateTime.Today.AddDays(31)))
                .WithMessage("The stay can’t be reserved more than 30 days in advance!");

            RuleFor(b => b)
                 .Must(m => {
                     return (m.End - m.Start).TotalDays <= 3;
                     })
                .WithMessage("The stay can’t be longer than 3 days!");
        }
    }
}
