using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Contracts.Commands;
using FagdagCqrs.Backend.Contracts.Queries;
using FagdagCqrs.Backend.Data.Adapters;
using FagdagCqrs.Backend.Data.Adapters.Commands;
using FagdagCqrs.Backend.Data.Adapters.Queries;
using FagdagCqrs.Database.Data;
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
            _roomBookingCommands = new RoomBookingCommands(TheDatabase.Instance());
            _roomBookingQueries = new RoomBookingQueries(TheDatabase.Instance());
            _roomTypeDefinitionDataAdapter = new RoomTypeDefinitionDataAdapter(TheDatabase.Instance());

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
                var bookingToCreate = this.Bind<CreateRoomBooking>();

                var newBookingId = Guid.NewGuid();

                _roomBookingCommands.Create(
                    newBookingId,
                    bookingToCreate.RoomType,
                    bookingToCreate.FromDate,
                    bookingToCreate.Duration,
                    _roomTypeDefinitionDataAdapter.Read(bookingToCreate.RoomType).PricePerNight * bookingToCreate.Duration
                    );

                return Response.AsJson(new IdWrapper(newBookingId));
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

        private static RoomBookingInfo MapToRoomBookingInfo(Data.Models.Queries.RoomBooking roomBooking)
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