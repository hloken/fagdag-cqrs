using System;
using System.Collections.Generic;
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

            Get["/bookingStatusTypes"] = parameters =>
            {
                var bookingStatusDictionary = GetEnumDictionary<RoomBookingStatus>();

                var bookingStatusTypes =
                    (from kvp in bookingStatusDictionary
                     select new BookingStatusType
                     {
                         Id = kvp.Key,
                         Title = kvp.Value
                     })
                     .ToArray();

                return Response.AsJson(bookingStatusTypes);
            };

            Post["/{bookingId}/confirm"] = parameters =>
            {
                Guid bookingId = parameters.bookingId;

                if (Database.RoomBookings.ContainsKey(bookingId))
                {
                    Database.RoomBookings[bookingId].Status = RoomBookingStatus.ConfirmedByCustomer;
                }

                return HttpStatusCode.OK;
            };

            Post[""] = parameters =>
            {
                var bookingToCreate = this.Bind<RoomBookingInfo>();

                var newBookingId = Guid.NewGuid();
                var roomBooking = CreateRoomBooking(newBookingId, bookingToCreate);
                roomBooking.Price = Database.RoomTypeDefinions[roomBooking.RoomType].PricePerNight * roomBooking.Duration;
                
                Database.RoomBookings.Add(newBookingId, roomBooking); 

                return Response.AsJson(new IdWrapper(newBookingId));
            };
        }

        private static Dictionary<int, string> GetEnumDictionary<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<T>()).ToDictionary(
                item => Convert.ToInt32(item),
                item => item.ToString());
        }

        private static RoomBooking CreateRoomBooking(Guid roomBookingId, RoomBookingInfo roomBookingToCreate)
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
            var roomBookingInfo = new RoomBookingInfo
            {
                Id = roomBooking.Id,
                Status = roomBooking.Status,
                RoomType = roomBooking.RoomType,
                FromDate = roomBooking.FromDate,
                Duration = roomBooking.Duration,
                Price = roomBooking.Price
            };

            return roomBookingInfo;
        }
    }
}