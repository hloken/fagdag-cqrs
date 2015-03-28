using System;
using Nancy;
using Nancy.ModelBinding;
using RestApi.Contracts;
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
                {
                    var roomBooking = Database.RoomBookings[bookingId];
                    var roomBookingInfo = new RoomBookingInfo(
                        roomBooking.Id, roomBooking.RoomType, roomBooking.FromDate, roomBooking.Duration);

                    return Response.AsJson(roomBookingInfo);
                }
                
                return HttpStatusCode.NotFound;
            };

            Post[""] = parameters =>
            {
                var roomBookingToCreate = this.Bind<RoomBookingInfo>();

                var roomBookingId = Guid.NewGuid();
                var roomBooking = RoomBooking.Create(roomBookingId, roomBookingToCreate.RoomType, roomBookingToCreate.FromDate, roomBookingToCreate.Duration);

                Database.RoomBookings.Add(roomBookingId, roomBooking); 

                return Response.AsJson(new IdWrapper(roomBookingId));
            };
        }     
    }
}