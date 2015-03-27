using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using RestApi.Contracts;
using RestApi.Data;

namespace RestApi.ApiModules
{
    public class RoomTypeModule : NancyModule
    {
        public RoomTypeModule()
            : base("api/room/types")
        {
            Get[""] = parameters =>
            {
                var enumDictionary = GetEnumDictionary();

                var roomTypeViewModels =
                    (from kvp in enumDictionary
                     select new RoomTypeInfo
                     {
                         Value = kvp.Key, 
                         Name = kvp.Value
                     })
                     .ToArray();

                return Response.AsJson(roomTypeViewModels);
            };
        }

        private static Dictionary<int, string> GetEnumDictionary()
        {
            return (Enum.GetValues(typeof (RoomType)).Cast<RoomType>()).ToDictionary(
                item => Convert.ToInt32(item), 
                item => item.ToString());
        }
    }
}