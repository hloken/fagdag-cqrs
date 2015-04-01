using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.DataModels;

namespace FagdagCqrs.Backend.DataAdapters
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