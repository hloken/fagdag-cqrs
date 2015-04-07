using System;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.Data.Models;
using FagdagCqrs.Database.Data;

namespace FagdagCqrs.Backend.Data.Adapters
{
    public class RoomBookingDataAdapter
    {
        private readonly TheDatabase _database;

        public RoomBookingDataAdapter(TheDatabase database)
        {
            _database = database;
        }

        public void Create(RoomBooking roomBookingToCreate)
        {
            var newBookingRow = MapToRoomBookingRow(roomBookingToCreate);
            
            _database.RoomBookingRows.Add(newBookingRow.Id, newBookingRow);
        }

        public RoomBooking Read(Guid bookingId)
        {
            if (_database.RoomBookingRows.ContainsKey(bookingId))
            {
                return MapToRoomBooking(_database.RoomBookingRows[bookingId]);
            }

            return null;
        }

        public ReadOnlyCollection<RoomBooking> ReadAll()
        {
            var roomBookings = (from roomBookingRow in _database.RoomBookingRows.Values
                                select MapToRoomBooking(roomBookingRow));

            return new ReadOnlyCollection<RoomBooking>(roomBookings.ToArray());
        }

        public void Update(RoomBooking updatedRoomBooking)
        {
            var updateBookingId = updatedRoomBooking.Id;

            if (_database.RoomBookingRows.ContainsKey(updateBookingId))
            {
                _database.RoomBookingRows.Remove(updateBookingId);
            }

            _database.RoomBookingRows[updateBookingId] = MapToRoomBookingRow(updatedRoomBooking);
        }

        private static RoomBookingRow MapToRoomBookingRow(RoomBooking roomBooking)
        {
            return new RoomBookingRow(
                roomBooking.Id,
                roomBooking.RoomType,
                roomBooking.FromDate,
                roomBooking.Duration,
                roomBooking.Price,
                roomBooking.Status
                );
        }

        private static RoomBooking MapToRoomBooking(RoomBookingRow roomBookingRow)
        {
            return new RoomBooking(
                roomBookingRow.Id,
                roomBookingRow.RoomType,
                roomBookingRow.FromDate,
                roomBookingRow.Duration,
                roomBookingRow.Price,
                roomBookingRow.Status);

        }
    }
}
