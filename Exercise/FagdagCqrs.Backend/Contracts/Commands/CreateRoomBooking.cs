using System;
using FagdagCqrs.Database.Contracts;

namespace FagdagCqrs.Backend.Contracts.Commands
{
    public class CreateRoomBooking
    {
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
    }
}
