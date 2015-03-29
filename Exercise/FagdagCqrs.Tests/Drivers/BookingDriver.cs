using System;
using System.Collections.Generic;
using FagdagCqrs.Backend.Contracts;
using FluentAssertions;
using Nancy.Testing;

namespace FagdagCqrs.Tests.Drivers
{
    public static class BookingDriver
    {
        private const string _baseUrl = "api/booking";

        public static BrowserResponse CreateBookingWithResponse(Browser browser, RoomBookingInfo bookingToCreate)
        {
            return browser.Post(_baseUrl, x => x.JsonBody(bookingToCreate));
        }

        public static Guid CreateBooking(Browser browser, RoomBookingInfo bookingToCreate)
        {
            return CreateBookingWithResponse(browser, bookingToCreate).Body.DeserializeJson<IdWrapper>().Id;
        }

        public static RoomBookingInfo GetBookingById(Browser browser, Guid bookingId)
        {
            var urlWithId = string.Format("{0}/{1}",_baseUrl, bookingId);
            var browserResponse = browser.Get(urlWithId);
            return browserResponse.Body.DeserializeJson<RoomBookingInfo>();
        }

        public static RoomBookingInfo[] GetAll(Browser browser)
        {
            return browser.Get(_baseUrl).Body.DeserializeJson<RoomBookingInfo[]>();
        }

        public static BookingStatusType[] GetBookingStatusTypes(Browser browser)
        {
            const string url = _baseUrl + "/bookingStatusTypes";
            return browser.Get(url).Body.DeserializeJson<BookingStatusType[]>();
        }

        public static BrowserResponse ConfirmBookingWithResponse(Browser browser, Guid bookingId)
        {
            var url = string.Format("{0}/{1}/confirm", _baseUrl, bookingId);
            return browser.Post(url);
        }

        public static void ShouldContainBooking(this IEnumerable<RoomBookingInfo> actualBookings, Guid bookingId, RoomBookingInfo expectedBooking)
        {
            actualBookings.Should().Contain(rbi =>
                rbi.Id == bookingId &&
                rbi.RoomType == expectedBooking.RoomType &&
                rbi.FromDate == expectedBooking.FromDate &&
                rbi.Duration == expectedBooking.Duration);
        }

        public static void ShouldContainRoomBookingStatus(this IEnumerable<BookingStatusType> actualBookingStatusTypes, RoomBookingStatus expectedRoomBookingStatus)
        {
            actualBookingStatusTypes.Should().Contain(rt =>
                rt.Title == expectedRoomBookingStatus.ToString() &&
                rt.Id == (int) expectedRoomBookingStatus);
        }
    }
}