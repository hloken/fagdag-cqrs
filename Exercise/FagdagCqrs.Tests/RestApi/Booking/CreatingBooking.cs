using System;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using FluentAssertions;
using Nancy;
using Nancy.Testing;
using RestApi.Contracts;
using RestApi.Data;

namespace FagdagCqrs.Tests.RestApi.Booking
{
    public class CreatingBooking : RestApiBddTestBase
    {
        private BrowserResponse _browserResponse;
        private IdWrapper _returnedBookingId;
        private RoomBooking _bookingToCreate;
        private RoomBooking _createdBooking;

        protected override void Given()
        {
        }

        protected override void When()
        {
            _bookingToCreate = new RoomBooking
            {
                RoomType = RoomType.Suite,
                FromDate = new DateTime(2014, 06, 05),
                Duration = 5
            };

            _browserResponse = BookingDriver.CreateBookingWithResponse(Browser, _bookingToCreate);
            _returnedBookingId = _browserResponse.Body.DeserializeJson<IdWrapper>();
            _createdBooking = BookingDriver.GetBookingById(Browser, _returnedBookingId.Id);
        }

        [Then]
        public void ShouldReturn200()
        {
            _browserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then]
        public void ShouldHaveBookingInfo()
        {
            _createdBooking.RoomType.Should().Be(_bookingToCreate.RoomType);
            _createdBooking.FromDate.Should().Be(_bookingToCreate.FromDate);
            _createdBooking.Duration.Should().Be(_bookingToCreate.Duration);
        }
    }
}
