using System;
using System.Collections.Generic;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.DataAdapters;
using FagdagCqrs.Backend.DataModels;
using Nancy;
using Nancy.ModelBinding;

namespace FagdagCqrs.Backend.ApiModules
{
    public class BookingModule : NancyModule
    {
        private readonly RoomBookingDataAdapter _roomBookingDataAdapter;
        private readonly RoomTypeDefinitionDataAdapter _roomTypeDefinitionDataAdapter;

        public BookingModule()
            : base("api/booking")
        {
            _roomBookingDataAdapter = new RoomBookingDataAdapter(Database.Instance());
            _roomTypeDefinitionDataAdapter = new RoomTypeDefinitionDataAdapter(Database.Instance());

            Get[""] = parameters =>
            {
                var bookings = _roomBookingDataAdapter.ReadAll();

                return Response.AsJson(bookings);
            };

            Get["/{bookingId}"] = parameters =>
            {
                Guid bookingId = parameters.bookingId;

                var roomBooking = _roomBookingDataAdapter.Read(bookingId);
                if (roomBooking != null)
                {
                    return Response.AsJson(
                        MapToRoomBookingInfo(roomBooking));
                }

                return HttpStatusCode.NotFound;
            };

            Post[""] = parameters =>
            {
                var bookingToCreate = this.Bind<RoomBookingInfo>();

                var newBookingId = Guid.NewGuid();
                var roomBooking = CreateRoomBooking(newBookingId, bookingToCreate);
                roomBooking.Price = _roomTypeDefinitionDataAdapter.Read(roomBooking.RoomType).PricePerNight * roomBooking.Duration;

                _roomBookingDataAdapter.Create(newBookingId, roomBooking);

                return Response.AsJson(new IdWrapper(newBookingId));
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

            Put["/{bookingId}/confirm"] = parameters =>
            {
                Guid bookingId = parameters.bookingId;

                var roomBooking = _roomBookingDataAdapter.Read(bookingId);
                if (roomBooking != null)
                {
                    roomBooking.Status = RoomBookingStatus.ConfirmedByCustomer;
                    _roomBookingDataAdapter.Update(roomBooking);

                    return HttpStatusCode.OK;
                }

                return HttpStatusCode.NotFound;
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