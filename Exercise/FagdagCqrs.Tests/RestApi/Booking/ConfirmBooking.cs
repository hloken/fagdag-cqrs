using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using FluentAssertions;
using Nancy;
using Nancy.Testing;

namespace FagdagCqrs.Tests.RestApi.Booking
{
    public class ConfirmBooking : RestApiBddTestBase
    {
        private BrowserResponse _browserResponse;
        private Guid _returnedBookingId;

        protected override void Given()
        {
            _returnedBookingId = BookingDriver.CreateBooking(Browser, new RoomBookingInfo(null, RoomType.Suite, new DateTime(2014, 06, 05), 5));
        }

        protected override void When()
        {
            _browserResponse = BookingDriver.ConfirmBookingWithResponse(Browser, _returnedBookingId);
        }

        [Then]
        public void ShouldReturn200()
        {
            _browserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then]
        public void BookingShouldBeConfirmed()
        {
            var booking = BookingDriver.GetBookingById(Browser, _returnedBookingId);

            booking.Status.Should().Be(RoomBookingStatus.ConfirmedByCustomer);
        }
    }
}