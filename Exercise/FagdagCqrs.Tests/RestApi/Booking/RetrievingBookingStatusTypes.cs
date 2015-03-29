using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;

namespace FagdagCqrs.Tests.RestApi.Booking
{
    public class RetrievingBookingStatusTypes : RestApiBddTestBase
    {
        private BookingStatusType[] _retrievedBookingStatusTypes;

        protected override void Given()
        {
        }

        protected override void When()
        {
            _retrievedBookingStatusTypes = BookingDriver.GetBookingStatusTypes(Browser);
        }

        [Then]
        public void ShouldHaveStandardRoomTypes()
        {
            _retrievedBookingStatusTypes.ShouldContainRoomBookingStatus(RoomBookingStatus.Draft);
            _retrievedBookingStatusTypes.ShouldContainRoomBookingStatus(RoomBookingStatus.ConfirmedByCustomer);
        }
    }
}