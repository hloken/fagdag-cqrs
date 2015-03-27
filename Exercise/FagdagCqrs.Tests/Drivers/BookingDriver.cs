using System;
using Nancy.Testing;
using RestApi.Data;

namespace FagdagCqrs.Tests.Drivers
{
    public class BookingDriver
    {
        private const string _url = "api/booking";

        public static BrowserResponse CreateBookingWithResponse(Browser browser, RoomBooking bookingToCreate)
        {
            return browser.Post(_url, x => x.JsonBody(bookingToCreate));
        }

        public static RoomBooking GetBookingById(Browser browser, Guid bookingId)
        {
            var urlWithId = string.Format("{0}/{1}",_url, bookingId);
            return browser.Get(urlWithId).Body.DeserializeJson<RoomBooking>();
        }
    }
}