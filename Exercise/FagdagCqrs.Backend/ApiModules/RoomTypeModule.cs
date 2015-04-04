using System;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data;
using FagdagCqrs.Backend.Data.Adapters;
using FagdagCqrs.Backend.Data.Internal;
using Nancy;

namespace FagdagCqrs.Backend.ApiModules
{
    public class RoomTypeModule : NancyModule
    {
        private RoomTypeDefinitionDataAdapter _roomTypeDefinitionDataAdapter;

        public RoomTypeModule() : base("api/roomtypes")
        {
            _roomTypeDefinitionDataAdapter = new RoomTypeDefinitionDataAdapter(Database.Instance());

            Get[""] = parameters =>
            {
                var roomTypeViewModels =
                    (from kvp in _roomTypeDefinitionDataAdapter.ReadAll()
                     select new RoomTypeInfo
                     {
                         Id = Convert.ToInt32(kvp.Key), 
                         Title = kvp.Value.RoomType.ToString(),
                         PricePerNight = kvp.Value.PricePerNight
                     })
                     .ToArray();

                return Response.AsJson(roomTypeViewModels);
            };
        }
    }
}