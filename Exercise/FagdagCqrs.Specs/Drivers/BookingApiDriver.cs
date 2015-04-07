using System;
using System.ComponentModel;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Specs.Arguments;

namespace FagdagCqrs.Specs.Drivers
{
    public class BookingApiDriver
    {
        private readonly HttpClientWrapper _client;

        public BookingApiDriver(HttpClientWrapper client)
        {
            _client = client;
        }

        public RoomBookingInfo FindBookingBy(RomType romType, DateTime fromDate, int duration)
        {
            RoomBookingInfo[] roomBookingInfos = _client.Get("api/booking", true).ToObject<RoomBookingInfo[]>();

            var firstMatchingBooking =
                roomBookingInfos.First(roomBookingInfo => roomBookingInfo.RoomType == GetRoomTypeFromRomType(romType) &&
                                                          roomBookingInfo.FromDate.Date == fromDate.Date &&
                                                          roomBookingInfo.Duration == duration);

            return firstMatchingBooking;
        }

        private RoomType GetRoomTypeFromRomType(RomType roomType)
        {
            switch (roomType)
            {
                case RomType.SuperDeluxeSuite:
                    return RoomType.SuperDeluxeSuite;
                case RomType.Suite:
                    return RoomType.Suite;
                case RomType.JuniorSuite:
                    return RoomType.JuniorSuite;
                case RomType.Dobbel:
                    return RoomType.Double;
                case RomType.Twin:
                    return RoomType.Twin;
                case RomType.Enkel:
                    return RoomType.Single;
                case RomType.Delt:
                    return RoomType.Shared;
            }

            throw new InvalidEnumArgumentException("Missing mapping for RomType");
        }
    }
}