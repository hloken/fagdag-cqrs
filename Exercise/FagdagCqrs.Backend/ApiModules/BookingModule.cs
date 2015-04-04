using System;
using System.Collections.Generic;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Contracts.Commands;
using FagdagCqrs.Backend.Contracts.Queries;
using FagdagCqrs.Backend.Data.Adapters;
using FagdagCqrs.Backend.Data.Adapters.Commands;
using FagdagCqrs.Backend.Data.Adapters.Queries;
using FagdagCqrs.Backend.Data.Internal;
using FagdagCqrs.Backend.Data.Models.Commands;
using Nancy;
using Nancy.ModelBinding;

namespace FagdagCqrs.Backend.ApiModules
{
    public class BookingModule : NancyModule
    {
        private readonly RoomBookingCommands _roomBookingCommands;
        private readonly RoomBookingQueries _roomBookingQueries;
        private readonly RoomTypeDefinitionDataAdapter _roomTypeDefinitionDataAdapter;

        public BookingModule()
            : base("api/booking")
        {
            _roomBookingCommands = new RoomBookingCommands(Database.Instance());
            _roomBookingQueries = new RoomBookingQueries(Database.Instance());
            _roomTypeDefinitionDataAdapter = new RoomTypeDefinitionDataAdapter(Database.Instance());

            Get[""] = parameters =>
            {
                var bookings = _roomBookingQueries.ReadAll();

                return Response.AsJson(bookings);
            };

            Get["/{bookingId}"] = parameters =>
            {
                Guid bookingId = parameters.bookingId;

                var roomBooking = _roomBookingQueries.Read(bookingId);
                if (roomBooking != null)
                {
                    return Response.AsJson(
                        MapToRoomBookingInfo(roomBooking));
                }

                return HttpStatusCode.NotFound;
            };

            Post[""] = parameters =>
            {
                var bookingToCreate = this.Bind<RoomBookingCommand>();

                var newBookingId = Guid.NewGuid();
                var roomBookingToCreate = CreateRoomBooking(newBookingId, bookingToCreate);
                roomBookingToCreate.Price = _roomTypeDefinitionDataAdapter.Read(roomBookingToCreate.RoomType).PricePerNight * roomBookingToCreate.Duration;

                _roomBookingCommands.Create(newBookingId, roomBookingToCreate);

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
                
                if (_roomBookingCommands.ConfirmBooking(bookingId))
                {
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

        private static RoomBooking CreateRoomBooking(Guid roomBookingId, RoomBookingCommand roomBookingToCreate)
        {
            var roomBooking = new RoomBooking(
                roomBookingId, 
                roomBookingToCreate.RoomType, 
                roomBookingToCreate.FromDate,
                roomBookingToCreate.Duration);

            return roomBooking;
        }

        private static RoomBookingInfo MapToRoomBookingInfo(Data.Models.Queries.RoomBooking roomBooking)
        {
            var roomBookingInfo = new RoomBookingInfo
            {
                Id = roomBooking.Id,
                RoomType = roomBooking.RoomType,
                FromDate = roomBooking.FromDate,
                Duration = roomBooking.Duration,
                Status = roomBooking.Status,
                Price = roomBooking.Price
            };

            return roomBookingInfo;
        }
    }
}