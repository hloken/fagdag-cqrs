using System;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Backend.Data;
using Nancy;

namespace FagdagCqrs.Backend.ApiModules
{
    public class RoomTypeModule : NancyModule
    {
        public RoomTypeModule() : base("api/roomtypes")
        {
            Get[""] = parameters =>
            {
                var roomTypeViewModels =
                    (from kvp in Database.RoomTypeDefinions
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