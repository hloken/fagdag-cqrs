using System.Collections.Generic;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Database.Contracts;
using FluentAssertions;
using Nancy.Testing;

namespace FagdagCqrs.Tests.Drivers
{
    public static class RoomTypeDriver
    {
        private const string _url = "api/roomtypes";

        public static RoomTypeInfo[] GetRoomTypeInfos(Browser browser)
        {
            return browser.Get(_url).Body.DeserializeJson<RoomTypeInfo[]>();
        }

        public static void ShouldContainRoomType(this IEnumerable<RoomTypeInfo> roomTypeInfos, RoomType roomType, decimal pricePerNight)
        {
            roomTypeInfos.Should().Contain(rt =>
                rt.Title == roomType.ToString() &&
                rt.Id == (int) roomType &&
                rt.PricePerNight == pricePerNight);
        }
    }
}