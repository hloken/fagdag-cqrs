using System;
using System.Collections.Generic;
using FagdagCqrs.Database.Contracts;

namespace FagdagCqrs.Database.Data
{
    public class TheDatabase
    {
        private static TheDatabase _instance;

        public Dictionary<Guid, RoomBookingRow> RoomBookingRows { get; private set; }
        public List<RoomTypeDefinitionRow> RoomTypeDefinionsRows { get; private set; }

        public static TheDatabase Instance()
        {
            if (_instance == null)
            {
                _instance = new TheDatabase();
            }

            return _instance;
        }

        private TheDatabase()
        {
            RoomBookingRows = new Dictionary<Guid, RoomBookingRow>();

            RoomTypeDefinionsRows = new List<RoomTypeDefinitionRow>();
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
            RoomTypeDefinionsRows.Add(new RoomTypeDefinitionRow(roomType, pricePerNight));
        }

        internal void Drop()
        {
            RoomBookingRows.Clear();
        }
    }
}