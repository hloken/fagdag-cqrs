using System;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.Data.Models
{
    public class RoomBooking
    {
        private RoomBooking(Guid id, RoomType roomType, DateTime fromDate, int duration)
        {
            Id = id;
            RoomType = roomType;
            FromDate = fromDate;
            Duration = duration;
            Status = RoomBookingStatus.Draft;
        }

        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public RoomBookingStatus Status { get; set; }
        public decimal? Price { get; set; }

        public static RoomBooking Create(Guid roomBookingId, RoomType roomType, DateTime fromDate, int duration)
        {
            return new RoomBooking(roomBookingId, roomType, fromDate, duration);
        }
    }
}