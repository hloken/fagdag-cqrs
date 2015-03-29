using System;

namespace FagdagCqrs.Backend.Contracts
{
    public class RoomBookingInfo
    {
        // Needed by Nancy Bind<>
        public RoomBookingInfo()
        {
        }

        public RoomBookingInfo(Guid? id, RoomType roomType, DateTime fromDate, int duration, decimal? price=null)
        {
            Id = id;
            RoomType = roomType;
            FromDate = fromDate;
            Duration = duration;
            Price = price;
        }

        public Guid? Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public RoomBookingStatus Status { get; set; }
        public decimal? Price { get; set; }
    }
}