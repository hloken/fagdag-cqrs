using System;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.Data.Models.Queries;
using FagdagCqrs.Database.Data;

namespace FagdagCqrs.Backend.Data.Adapters.Queries
{
    public class RoomBookingQueries
    {
        private readonly TheDatabase _database;

        public RoomBookingQueries(TheDatabase database)
        {
            _database = database;
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