using System;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data.Internal;
using FagdagCqrs.Backend.Data.Models.Commands;

namespace FagdagCqrs.Backend.Data.Adapters.Commands
{
    public class RoomBookingCommands
    {
        private readonly Database _database;

        public RoomBookingCommands(Database database)
        {
            _database = database;
        }

        public void Create(Guid bookingId, RoomBooking roomBookingToCreate)
        {
            var roomBookingInternal = CreateRoomBooking(bookingId, roomBookingToCreate);
            _database.RoomBookings.Add(bookingId, roomBookingInternal);
        }

        public bool ConfirmBooking(Guid confirmBookingId)
        {
            if (_database.RoomBookings.ContainsKey(confirmBookingId))
            {
                _database.RoomBookings[confirmBookingId].Status = RoomBookingStatus.ConfirmedByCustomer;

                return true;
            }

            return false;
        }

        private static RoomBookingInternal CreateRoomBooking(Guid roomBookingId, RoomBooking roomBookingToCreate)
        {
            var roomBooking = RoomBookingInternal.Create(
                roomBookingId,
                roomBookingToCreate.RoomType,
                roomBookingToCreate.FromDate,
                roomBookingToCreate.Duration,
                roomBookingToCreate.Price);

            return roomBooking;
        }
    }
}
