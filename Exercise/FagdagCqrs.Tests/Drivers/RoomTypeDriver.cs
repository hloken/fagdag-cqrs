using System.Collections.Generic;
using FluentAssertions;
using Nancy.Testing;
using RestApi.Contracts;
using RestApi.Data;

namespace FagdagCqrs.Tests.Drivers
{
    public static class RoomTypeDriver
    {
        private const string _url = "api/roomtypes";

        public static RoomTypeInfo[] GetRoomTypeInfos(Browser browser)
        {
            return browser.Get(_url).Body.DeserializeJson<RoomTypeInfo[]>();
        }

        public static void ShouldContainRoomType(this IEnumerable<RoomTypeInfo> roomTypeInfos, RoomType roomType)
        {
            roomTypeInfos.Should().Contain(rt =>
                rt.Title == roomType.ToString() &&
                rt.Id == (int)roomType);
        }
    }
}