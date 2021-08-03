using Xunit;
using BookChallenge;
using System;
using System.Collections.Generic;
using System.Text;
using BookChallenge.Models;
using FluentValidation.TestHelper;

namespace BookChallenge.Tests
{
    public class BookingValidatorTests
    {
        private readonly BookingValidator _validator;

        public BookingValidatorTests()
        {
            _validator = new BookingValidator();
        }
        
        [Fact()]
        public void Should_have_error_when_CustomerName_is_null()
        {
            var model = new Booking { CustomerName = null };
            var actual = _validator.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(s => s.CustomerName);
        }

        [Fact()]
        public void Should_have_error_when_CustomerName_is_empty()
        {
            var model = new Booking { CustomerName = "" };
            var actual = _validator.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(s => s.CustomerName);
        }

        [Fact()]
        public void Should_have_error_when_StartDate_is_less_than_tomorrow()
        {
            var model = new Booking { 
                CustomerName = "fooBar",
                Start = DateTime.MinValue
            };

            var actual = _validator.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(s => s.Start);
        }

        [Fact()]
        public void Should_not_have_error_when_StartDate_is_greater_than_tomorrow()
        {
            var model = new Booking { 
                CustomerName = "fooBar",
                Start = DateTime.Today.AddDays(1)
            };

            var actual = _validator.TestValidate(model);

            actual.ShouldNotHaveValidationErrorFor(s => s.Start);
        }

        [Fact()]
        public void Should_have_error_when_StartDate_is_greater_than_30d()
        {
            var model = new Booking { 
                CustomerName = "fooBar",
                Start = DateTime.Today.AddDays(31)
            };

            var actual = _validator.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(s => s.Start);
        }

        [Fact()]
        public void Should_not_have_error_when_StartDate_30d()
        {
            var model = new Booking { 
                CustomerName = "fooBar",
                Start = DateTime.Today.AddDays(30).AddHours(4)
            };

            var actual = _validator.TestValidate(model);

            actual.ShouldNotHaveValidationErrorFor(s => s.Start);
        }
        
        [Fact()]
        public void Should_have_error_when_booking_last_longer_than_3d()
        {
            var startDate = DateTime.Today.AddDays(30).AddHours(4);
            var model = new Booking { 
                CustomerName = "fooBar",
                Start = startDate,
                End = startDate.AddDays(10).AddHours(4)
            };

            var actual = _validator.TestValidate(model);

            actual.ShouldHaveValidationErrorFor(s => s);
        } 

        [Fact()]
        public void Should_not_have_error_when_booking_last_2d()
        {
            var startDate = DateTime.Today.AddDays(10);
            var model = new Booking { 
                CustomerName = "fooBar",
                Start = startDate,
                End = startDate.AddDays(2).AddHours(4)
            };

            var actual = _validator.TestValidate(model);

            actual.ShouldNotHaveValidationErrorFor(s => s);
        }
    }
}