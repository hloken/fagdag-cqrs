using System;
using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.Data.Models.Commands
{
    public class RoomBooking
    {
        public RoomBooking(Guid id, RoomType roomType, DateTime fromDate, int duration)
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
        public decimal? Price { get; set; }
    }
}