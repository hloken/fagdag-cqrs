using System;
using Nancy;
using Nancy.ModelBinding;
using RestApi.Data;

namespace RestApi.ApiModules
{
    public class BookingModule : NancyModule
    {
        public BookingModule()
            : base("api/booking")
        {
            Get["/{bookingId}"] = parameters =>
            {
                Guid bookingId = parameters.bookingId;

                if (Database.RoomBookings.ContainsKey(bookingId))
                    return Response.AsJson(Database.RoomBookings[bookingId]);
                
                return HttpStatusCode.NotFound;
            };

            Post[""] = parameters =>
            {
                var roomBookingToCreate = this.Bind<RoomBooking>();

                var roomBookingId = Guid.NewGuid();
                Database.RoomBookings.Add(roomBookingId, roomBookingToCreate);

                return Response.AsJson(new {id = roomBookingId});
            };
        }     
    }
}