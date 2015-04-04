using System;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.Data.Internal
{
    public class RoomBookingInternal
    {
        private RoomBookingInternal(Guid id, RoomType roomType, DateTime fromDate, int duration, decimal? price)
        {
            Id = id;
            RoomType = roomType;
            FromDate = fromDate;
            Duration = duration;
            Status = RoomBookingStatus.Draft;
            Price = price;
        }

        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public RoomBookingStatus Status { get; set; }
        public decimal? Price { get; set; }

        public static RoomBookingInternal Create(Guid roomBookingId, RoomType roomType, DateTime fromDate, int duration, decimal? price)
        {
            return new RoomBookingInternal(roomBookingId, roomType, fromDate, duration, price);
        }
    }
}