using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data.Adapters;
using FagdagCqrs.Backend.Data.Models;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Database.Data;
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
            _roomBookingDataAdapter = new RoomBookingDataAdapter(TheDatabase.Instance());
            _roomTypeDefinitionDataAdapter = new RoomTypeDefinitionDataAdapter(TheDatabase.Instance());

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
                var roomBooking = MapToRoomBooking(bookingToCreate);

                var newBookingId = Guid.NewGuid();
                roomBooking.Id = newBookingId;
                roomBooking.Price = _roomTypeDefinitionDataAdapter.Read(roomBooking.RoomType).PricePerNight * roomBooking.Duration;
                roomBooking.Status = RoomBookingStatus.Draft;

                _roomBookingDataAdapter.Create(roomBooking);

                return Response.AsJson(new IdWrapper(newBookingId));
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
        
        private static RoomBooking MapToRoomBooking(RoomBookingInfo roomBookingToCreate)
        {
            var roomBooking = new RoomBooking(
                roomBookingToCreate.Id ?? Guid.Empty, 
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