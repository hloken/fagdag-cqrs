using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using FluentAssertions;

namespace FagdagCqrs.Tests.RestApi.Booking
{
    public class RetrievingAllBookings : RestApiBddTestBase
    {
        private RoomBookingInfo _firstBookingToCreate;
        private RoomBookingInfo _secondBookingToCreate;
        private Guid _firstBookingId;
        private Guid _secondBookingId;
        private RoomBookingInfo[] _actualBookings;

        protected override void Given()
        {
            _firstBookingToCreate = new RoomBookingInfo(null, RoomType.Single, new DateTime(2015, 06, 27), 3);
            _firstBookingId = BookingDriver.CreateBooking(Browser, _firstBookingToCreate); 
            
            _secondBookingToCreate = new RoomBookingInfo(null, RoomType.SuperDeluxeSuite, new DateTime(2015, 05, 16), 2);
            _secondBookingId = BookingDriver.CreateBooking(Browser, _secondBookingToCreate);
        }

        protected override void When()
        {
            _actualBookings = BookingDriver.GetAll(Browser);
        }

        [Then]
        public void ShouldHaveTwoBookings()
        {
            _actualBookings.Should().HaveCount(2);
        }

        [Then]
        public void ShouldContainFirstBooking()
        {
            _actualBookings.ShouldContainBooking(_firstBookingId, _firstBookingToCreate);
        }

        [Then]
        public void ShouldContainSecondBooking()
        {
            _actualBookings.ShouldContainBooking(_secondBookingId, _secondBookingToCreate);
        }
    }
}