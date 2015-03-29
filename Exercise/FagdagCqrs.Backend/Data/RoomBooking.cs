using System;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.Data
{
    public class RoomBooking
    {
        private RoomBooking(Guid id, RoomType roomType, DateTime fromDate, int duration)
        {
            Id = id;
            RoomType = roomType;
            FromDate = fromDate;
            Duration = duration;
        }

        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }

        public static RoomBooking Create(Guid roomBookingId, RoomType roomType, DateTime fromDate, int duration)
        {
            return new RoomBooking(roomBookingId, roomType, fromDate, duration);
        }
    }
}