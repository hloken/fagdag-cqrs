using System;
using FagdagCqrs.Backend.Contracts.Queries;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using FluentAssertions;
using Nancy;
using Nancy.Testing;

namespace FagdagCqrs.Tests.RestApi.Testing
{
    public class DroppingDatabase : RestApiBddTestBase
    {
        private BrowserResponse _browserResponse;

        protected override void Given()
        {
            BookingDriver.CreateBooking(Browser, new RoomBookingInfo(null, RoomType.Single, new DateTime(2016, 07, 27), 7));
        }

        protected override void When()
        {
            _browserResponse = TestingDriver.DropDatabaseWithResponse(Browser);
        }

        [Then]
        public void ShouldReturn200()
        {
            _browserResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then]
        public void ShouldHaveClearedBookings()
        {
            var bookings = BookingDriver.GetAll(Browser);

            bookings.Should().BeEmpty();
        }
    }
}