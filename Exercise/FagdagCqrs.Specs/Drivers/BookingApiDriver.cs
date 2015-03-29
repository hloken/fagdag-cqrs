using System;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Specs.Drivers
{
    public class BookingApiDriver
    {
        private readonly HttpClientWrapper _client;

        public BookingApiDriver(HttpClientWrapper client)
        {
            _client = client;
        }

        public RoomBookingInfo FindBookingBy(RoomType roomType, DateTime fromDate, int duration)
        {
            string jsonResult = _client.Get("api/booking", true);

            return null;
        }
    }
}