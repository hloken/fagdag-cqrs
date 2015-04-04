using System;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.DataModels;

namespace FagdagCqrs.Backend.DataAdapters.Commands
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
            _database.RoomBookings.Add(bookingId, roomBookingToCreate);
        }

        public void Update(RoomBooking updatedRoomBooking)
        {
            var updateBookingId = updatedRoomBooking.Id;

            if (_database.RoomBookings.ContainsKey(updateBookingId))
            {
                _database.RoomBookings.Remove(updateBookingId);
            }

            _database.RoomBookings[updateBookingId] = updatedRoomBooking;
        }
    }
}
