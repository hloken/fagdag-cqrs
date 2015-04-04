using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Contracts.Commands;
using FagdagCqrs.Backend.Contracts.Queries;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using FluentAssertions;

namespace FagdagCqrs.Tests.RestApi.Booking
{
    public class RetrievingAllBookings : RestApiBddTestBase
    {
        private RoomBookingCommand _firstBookingToCreate;
        private RoomBookingCommand _secondBookingToCreate;
        private Guid _firstBookingId;
        private Guid _secondBookingId;
        private RoomBookingInfo[] _actualBookings;

        protected override void Given()
        {
            _firstBookingToCreate = new RoomBookingCommand(null, RoomType.Single, new DateTime(2015, 06, 27), 3);
            _firstBookingId = BookingDriver.CreateBooking(Browser, _firstBookingToCreate); 
            
            _secondBookingToCreate = new RoomBookingCommand(null, RoomType.SuperDeluxeSuite, new DateTime(2015, 05, 16), 2);
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