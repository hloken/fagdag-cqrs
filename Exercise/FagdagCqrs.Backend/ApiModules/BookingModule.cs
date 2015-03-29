using System;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data;
using Nancy;
using Nancy.ModelBinding;

namespace FagdagCqrs.Backend.ApiModules
{
    public class BookingModule : NancyModule
    {
        public BookingModule()
            : base("api/booking")
        {
            Get[""] = parameters =>
            {
                var bookings = Database.RoomBookings.Values.Select(MapToRoomBookingInfo)
                     .ToArray();

                return Response.AsJson(bookings);
            };

            Get["/{bookingId}"] = parameters =>
            {
                Guid bookingId = parameters.bookingId;

                if (Database.RoomBookings.ContainsKey(bookingId))
                {
                    var roomBooking = Database.RoomBookings[bookingId];

                    return Response.AsJson(
                        MapToRoomBookingInfo(roomBooking));
                }

                return HttpStatusCode.NotFound;
            };

            Post[""] = parameters =>
            {
                var bookingToCreate = this.Bind<RoomBookingInfo>();

                var newBookingId = Guid.NewGuid();
                Database.RoomBookings.Add(newBookingId, MapToRoomBooking(newBookingId, bookingToCreate)); 

                return Response.AsJson(new IdWrapper(newBookingId));
            };
        }

        private static RoomBooking MapToRoomBooking(Guid roomBookingId, RoomBookingInfo roomBookingToCreate)
        {
            var roomBooking = RoomBooking.Create(
                roomBookingId, 
                roomBookingToCreate.RoomType, 
                roomBookingToCreate.FromDate,
                roomBookingToCreate.Duration);

            return roomBooking;
        }

        private static RoomBookingInfo MapToRoomBookingInfo(RoomBooking roomBooking)
        {
            var roomBookingInfo = new RoomBookingInfo(
                roomBooking.Id, 
                roomBooking.RoomType, 
                roomBooking.FromDate, 
                roomBooking.Duration);

            return roomBookingInfo;
        }
    }
}