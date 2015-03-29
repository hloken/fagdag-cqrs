using System;
using System.Collections.Generic;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.Data
{
    public static class Database
    {
        public static Dictionary<Guid, RoomBooking> RoomBookings { get; private set; }
        public static Dictionary<RoomType, RoomTypeDefinition> RoomTypeDefinions { get; private set; }

        static Database()
        {
            RoomBookings = new Dictionary<Guid, RoomBooking>();

            RoomTypeDefinions = new Dictionary<RoomType, RoomTypeDefinition>();
            AddRoomTypeDefinition(RoomType.Shared, 280.0m);
            AddRoomTypeDefinition(RoomType.Single, 540.0m);
            AddRoomTypeDefinition(RoomType.Double, 750.0m);
            AddRoomTypeDefinition(RoomType.Twin, 750.0m);
            AddRoomTypeDefinition(RoomType.JuniorSuite, 1100.0m);
            AddRoomTypeDefinition(RoomType.Suite, 1700.0m);
            AddRoomTypeDefinition(RoomType.SuperDeluxeSuite, 2750.0m);
        }

        private static void AddRoomTypeDefinition(RoomType roomType, decimal pricePerNight)
        {
            RoomTypeDefinions.Add(roomType, new RoomTypeDefinition(roomType, pricePerNight));
        }
        
        internal static void Drop()
        {
            RoomBookings.Clear();
        }
    }
}