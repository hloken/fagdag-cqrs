using System;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.Data.Internal;
using FagdagCqrs.Backend.Data.Models.Queries;

namespace FagdagCqrs.Backend.Data.Adapters.Queries
{
    public class RoomBookingQueries
    {
        private readonly Database _database;

        public RoomBookingQueries(Database database)
        {
            _database = database;
        }

        public RoomBooking Read(Guid bookingId)
        {
            if (_database.RoomBookings.ContainsKey(bookingId))
            {
                var rb = _database.RoomBookings[bookingId];
                return CreateQueriesRoomBooking(rb);
            }

            return null;
        }

        public ReadOnlyCollection<RoomBooking> ReadAll()
        {
            var roomBookings = (from rb in _database.RoomBookings.Values
                                select CreateQueriesRoomBooking(rb));

            return new ReadOnlyCollection<RoomBooking>(roomBookings.ToArray());
        }

        private static RoomBooking CreateQueriesRoomBooking(RoomBookingInternal rb)
        {
            return new RoomBooking(
                rb.Id,
                rb.RoomType,
                rb.FromDate,
                rb.Duration,
                rb.Status,
                rb.Price
                );
        }
    }
}