using System;
using System.Collections.Generic;
using FluentAssertions;
using Nancy.Testing;
using RestApi.Contracts;

namespace FagdagCqrs.Tests.Drivers
{
    public static class BookingDriver
    {
        private const string _url = "api/booking";

        public static BrowserResponse CreateBookingWithResponse(Browser browser, RoomBookingInfo bookingToCreate)
        {
            return browser.Post(_url, x => x.JsonBody(bookingToCreate));
        }

        public static Guid CreateBooking(Browser browser, RoomBookingInfo bookingToCreate)
        {
            return CreateBookingWithResponse(browser, bookingToCreate).Body.DeserializeJson<IdWrapper>().Id;
        }

        public static RoomBookingInfo GetBookingById(Browser browser, Guid bookingId)
        {
            var urlWithId = string.Format("{0}/{1}",_url, bookingId);
            return browser.Get(urlWithId).Body.DeserializeJson<RoomBookingInfo>();
        }

        public static RoomBookingInfo[] GetAll(Browser browser)
        {
            return browser.Get(_url).Body.DeserializeJson<RoomBookingInfo[]>();
        }

        public static void ShouldContainBooking(this IEnumerable<RoomBookingInfo> actualBookings, Guid bookingId, RoomBookingInfo expectedBooking)
        {
            actualBookings.Should().Contain(rbi =>
                rbi.Id == bookingId &&
                rbi.RoomType == expectedBooking.RoomType &&
                rbi.FromDate == expectedBooking.FromDate &&
                rbi.Duration == expectedBooking.Duration);
        }
    }
}