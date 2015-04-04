using System;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.Data.Models;

namespace FagdagCqrs.Backend.Data.Adapters
{
    public class RoomBookingDataAdapter
    {
        private readonly Database _database;

        public RoomBookingDataAdapter(Database database)
        {
            _database = database;
        }

        public void Create(Guid bookingId, RoomBooking roomBookingToCreate)
        {
            _database.RoomBookings.Add(bookingId, roomBookingToCreate);
        }

        public RoomBooking Read(Guid bookingId)
        {
            if (_database.RoomBookings.ContainsKey(bookingId))
            {
                return _database.RoomBookings[bookingId];
            }

            return null;
        }

        public ReadOnlyCollection<RoomBooking> ReadAll()
        {
            var roomBookings = (from roomBooking in _database.RoomBookings.Values
                select roomBooking);

            return new ReadOnlyCollection<RoomBooking>(roomBookings.ToArray());
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
