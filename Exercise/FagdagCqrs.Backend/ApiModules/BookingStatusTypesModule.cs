using System;
using System.Collections.Generic;
using System.Linq;
using FagdagCqrs.Backend.Contracts;
using FagdagCqrs.Database.Contracts;
using Nancy;

namespace FagdagCqrs.Backend.ApiModules
{
    public class BookingStatusTypesModule  : NancyModule
    {
        public BookingStatusTypesModule()
            : base("api/booking")
        {
            Get["/bookingStatusTypes"] = parameters =>
            {
                var bookingStatusDictionary = GetEnumDictionary<RoomBookingStatus>();

                var bookingStatusTypes =
                    (from kvp in bookingStatusDictionary
                     select new BookingStatusType
                     {
                         Id = kvp.Key,
                         Title = kvp.Value
                     })
                     .ToArray();

                return Response.AsJson(bookingStatusTypes);
            };
        }

        private static Dictionary<int, string> GetEnumDictionary<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<T>()).ToDictionary(
                item => Convert.ToInt32(item),
                item => item.ToString());
        }
    }
}