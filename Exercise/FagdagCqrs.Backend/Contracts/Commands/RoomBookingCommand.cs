using System;

namespace FagdagCqrs.Backend.Contracts.Commands
{
    public class RoomBookingCommand
    {
        // Needed by Nancy Bind<>
        public RoomBookingCommand()
        {
        }

        public RoomBookingCommand(Guid? id, RoomType roomType, DateTime fromDate, int duration)
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