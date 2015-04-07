using System.Collections.Generic;
using System.Linq;
using FagdagCqrs.Backend.Data.Models;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Database.Data;

namespace FagdagCqrs.Backend.Data.Adapters
{
    public class RoomTypeDefinitionDataAdapter
    {
        private readonly TheDatabase _database;

        public RoomTypeDefinitionDataAdapter(TheDatabase database)
        {
            _database = database;
        }

        public RoomTypeDefinition Read(RoomType roomType)
        {
            var roomTypeDefinition = (from row in _database.RoomTypeDefinionsRows
                                      where row.RoomType == roomType
                                      select MapToRoomTypeDefinition(row))
                                    .SingleOrDefault();
            return roomTypeDefinition;
        }

        public IEnumerable<RoomTypeDefinition> ReadAll()
        {
            var roomTypeDefinitions = (from row in _database.RoomTypeDefinionsRows
                select MapToRoomTypeDefinition(row))
                .ToList();

            return roomTypeDefinitions.AsReadOnly();
        }

        private static RoomTypeDefinition MapToRoomTypeDefinition(RoomTypeDefinitionRow roomTypeDefinionRow)
        {
            return new RoomTypeDefinition(roomTypeDefinionRow.RoomType, roomTypeDefinionRow.PricePerNight);
        }
    }
}