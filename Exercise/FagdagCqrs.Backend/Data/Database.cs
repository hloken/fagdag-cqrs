using System;
using System.Collections.Generic;

namespace RestApi.Data
{
    public static class Database
    {
        private static readonly Dictionary<Guid, RoomBooking> _roomBookings = new Dictionary<Guid, RoomBooking>();

        public static Dictionary<Guid, RoomBooking> RoomBookings
        {
            get { return _roomBookings; }
        }

        internal static void Drop()
        {
            RoomBookings.Clear();
        }
    }
}