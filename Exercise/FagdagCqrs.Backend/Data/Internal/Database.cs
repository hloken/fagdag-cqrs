using System;
using System.Collections.Generic;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data.Models;

namespace FagdagCqrs.Backend.Data.Internal
{
    public class Database
    {
        private static Database _instance;

        public Dictionary<Guid, RoomBookingInternal> RoomBookings { get; private set; }
        public Dictionary<RoomType, RoomTypeDefinition> RoomTypeDefinions { get; private set; }

        public static Database Instance()
        {
            if (_instance == null)
            {
                _instance = new Database();
            }

            return _instance;
        }

        private Database()
        {
            RoomBookings = new Dictionary<Guid, RoomBookingInternal>();

            RoomTypeDefinions = new Dictionary<RoomType, RoomTypeDefinition>();
            AddRoomTypeDefinition(RoomType.Shared, 280.0m);
            AddRoomTypeDefinition(RoomType.Single, 540.0m);
            AddRoomTypeDefinition(RoomType.Double, 750.0m);
            AddRoomTypeDefinition(RoomType.Twin, 750.0m);
            AddRoomTypeDefinition(RoomType.JuniorSuite, 1100.0m);
            AddRoomTypeDefinition(RoomType.Suite, 1700.0m);
            AddRoomTypeDefinition(RoomType.SuperDeluxeSuite, 2750.0m);
        }

        private void AddRoomTypeDefinition(RoomType roomType, decimal pricePerNight)
        {
            RoomTypeDefinions.Add(roomType, new RoomTypeDefinition(roomType, pricePerNight));
        }

        internal void Drop()
        {
            RoomBookings.Clear();
        }
    }
}