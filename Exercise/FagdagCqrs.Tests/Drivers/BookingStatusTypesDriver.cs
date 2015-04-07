using FagdagCqrs.Backend.Contracts;
using Nancy.Testing;

namespace FagdagCqrs.Tests.Drivers
{
    public class BookingStatusTypesDriver
    {
        public static BookingStatusType[] GetBookingStatusTypes(Browser browser)
        {
            const string url = "api/booking/bookingStatusTypes";
            return browser.Get(url).Body.DeserializeJson<BookingStatusType[]>();
        } 
    }
}