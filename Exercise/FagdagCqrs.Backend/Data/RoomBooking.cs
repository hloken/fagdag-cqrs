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
            Status = RoomBookingStatus.Draft;
        }

        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public RoomBookingStatus Status { get; set; }
        public decimal? Price { get; set; }

        public void UpdateFrom(RoomBooking updatedRoomBooking)
        {
            Id = updatedRoomBooking.Id;
            RoomType = updatedRoomBooking.RoomType;
            FromDate = updatedRoomBooking.FromDate;
            Duration  = updatedRoomBooking.Duration;
            Status = updatedRoomBooking.Status;
            Price = updatedRoomBooking.Price;
        }

        public static RoomBooking Create(Guid roomBookingId, RoomType roomType, DateTime fromDate, int duration)
        {
            return new RoomBooking(roomBookingId, roomType, fromDate, duration);
        }
    }
}