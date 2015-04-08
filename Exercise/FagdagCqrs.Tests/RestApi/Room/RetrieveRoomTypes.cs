using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;

namespace FagdagCqrs.Tests.RestApi.Room
{
    public class RetrieveRoomTypes : RestApiBddTestBase
    {
        private RoomTypeInfo[] _retrievedRoomTypes;

        protected override void Given()
        {
        }

        protected override void When()
        {
            _retrievedRoomTypes = RoomTypeDriver.GetRoomTypeInfos(Browser);
        }

        [Then]
        public void ShouldHaveStandardRoomTypes()
        {
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Shared, 280.0m);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Single, 540.0m);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Double, 750.0m);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Twin, 750.0m);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.JuniorSuite, 1100.0m);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Suite, 1700.0m);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.SuperDeluxeSuite, 2750.0m);
        }
    }
}