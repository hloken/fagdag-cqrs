using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using FluentAssertions;
using Nancy;
using Nancy.Testing;

namespace FagdagCqrs.Tests.RestApi.Booking
{
    public class CreatingBooking : RestApiBddTestBase
    {
        private BrowserResponse _browserResponse;
        private IdWrapper _returnedBookingId;
        private RoomBookingInfo _bookingToCreate;
        private RoomBookingInfo _createdBooking;

        protected override void Given()
        {
        }

        protected override void When()
        {
            _bookingToCreate = new RoomBookingInfo(null, RoomType.Suite, new DateTime(2014, 06, 05), 5);

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
