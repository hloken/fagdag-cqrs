using System;

namespace FagdagCqrs.Backend.Contracts.Queries
{
    public class RoomBookingInfo
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public RoomBookingStatus Status { get; set; }
        public decimal? Price { get; set; }
    }
}
