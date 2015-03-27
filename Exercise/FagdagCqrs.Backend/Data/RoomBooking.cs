using System;

namespace RestApi.Data
{
    public class RoomBooking
    {
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
    }
}