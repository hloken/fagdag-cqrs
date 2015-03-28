using System;

namespace RestApi.Contracts
{
    public class RoomBookingInfo
    {
        // Needed by Nancy Bind<>
        public RoomBookingInfo()
        {
        }

        public RoomBookingInfo(Guid? id, RoomType roomType, DateTime fromDate, int duration)
        {
            Id = id;
            RoomType = roomType;
            FromDate = fromDate;
            Duration = duration;
        }

        public Guid? Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; } 
    }
}