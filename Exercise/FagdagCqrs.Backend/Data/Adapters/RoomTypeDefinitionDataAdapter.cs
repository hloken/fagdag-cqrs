using System.Collections.ObjectModel;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data.Models;

namespace FagdagCqrs.Backend.Data.Adapters
{
    public class RoomTypeDefinitionDataAdapter
    {
        private readonly Database _database;

        public RoomTypeDefinitionDataAdapter(Database database)
        {
            _database = database;
        }

        public RoomTypeDefinition Read(RoomType roomType)
        {
            if (_database.RoomTypeDefinions.ContainsKey(roomType))
            {
                return _database.RoomTypeDefinions[roomType];
            }

            return null;
        }

        public ReadOnlyDictionary<RoomType, RoomTypeDefinition> ReadAll()
        {
            return new ReadOnlyDictionary<RoomType, RoomTypeDefinition>(_database.RoomTypeDefinions);
        }
    }
}