using FagdagCqrs.Tests.Bdd;
using FagdagCqrs.Tests.Drivers;
using RestApi.Contracts;
using RestApi.Data;

namespace FagdagCqrs.Tests.RestApi.Room
{
    public class RetrieRoomTypes : RestApiBddTestBase
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
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Shared);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Single);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Double);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.JuniorSuite);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.Suite);
            _retrievedRoomTypes.ShouldContainRoomType(RoomType.SuperDeluxeSuite);
        }
    }
}