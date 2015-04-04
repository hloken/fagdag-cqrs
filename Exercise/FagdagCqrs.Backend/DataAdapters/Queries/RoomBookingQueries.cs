using System;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.DataModels;

namespace FagdagCqrs.Backend.DataAdapters.Queries
{
    public class RoomBookingQueries
    {
        private readonly Database _database;

        public RoomBookingQueries(Database database)
        {
            this._database = database;
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
    }
}