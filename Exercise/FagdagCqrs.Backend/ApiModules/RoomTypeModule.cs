using System;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data.Adapters;
using FagdagCqrs.Database.Data;
using Nancy;

namespace FagdagCqrs.Backend.ApiModules
{
    public class RoomTypeModule : NancyModule
    {
        private readonly RoomTypeDefinitionDataAdapter _roomTypeDefinitionDataAdapter;

        public RoomTypeModule() : base("api/roomtypes")
        {
            _roomTypeDefinitionDataAdapter = new RoomTypeDefinitionDataAdapter(TheDatabase.Instance());

            Get[""] = parameters =>
            {
                var roomTypeViewModels =
                    (from roomTypeDefinition in _roomTypeDefinitionDataAdapter.ReadAll()
                     select new RoomTypeInfo
                     {
                         Id = Convert.ToInt32(roomTypeDefinition.RoomType),
                         Title = roomTypeDefinition.RoomType.ToString(),
                         PricePerNight = roomTypeDefinition.PricePerNight
                     })
                     .ToArray();

                return Response.AsJson(roomTypeViewModels);
            };
        }
    }
}