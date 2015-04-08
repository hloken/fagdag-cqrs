using System;
using FagdagCqrs.Database.Contracts;
using FagdagCqrs.Database.Data;

namespace FagdagCqrs.Backend.Data.Adapters.Commands
{
    public class RoomBookingCommands
    {
        private readonly TheDatabase _database;

        public RoomBookingCommands(TheDatabase database)
        {
            _database = database;
        }

        public void Create(Guid bookingId, RoomType roomType, DateTime fromDate, int duration, decimal? price)
        {
            var newBookingRow = new RoomBookingRow(bookingId, roomType, fromDate, duration, price, RoomBookingStatus.Draft);
            
            _database.RoomBookingRows.Add(newBookingRow.Id, newBookingRow);
        }

        public bool ConfirmBooking(Guid bookingId)
        {
            if (_database.RoomBookingRows.ContainsKey(bookingId))
            {
                _database.RoomBookingRows[bookingId].Status = RoomBookingStatus.ConfirmedByCustomer;

                return true;
            }

            return false;
        }
    }
}
