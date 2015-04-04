using System;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.Data.Models.Queries
{
    public class RoomBooking
    {
        public RoomBooking(Guid id, RoomType roomType, DateTime fromDate, int duration, RoomBookingStatus status, decimal? price)
        {
            Id = id;
            RoomType = roomType;
            FromDate = fromDate;
            Duration = duration;
            Status = status;
            Price = price;
        }

        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public RoomBookingStatus Status { get; set; }
        public decimal? Price { get; set; }
    }
}