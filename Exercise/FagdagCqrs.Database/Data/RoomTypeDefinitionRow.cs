using FagdagCqrs.Database.Contracts;

namespace FagdagCqrs.Database.Data
{
    public class RoomTypeDefinitionRow
    {
        public RoomTypeDefinitionRow(RoomType roomType, decimal pricePerNight)
        {
            RoomType = roomType;
            PricePerNight = pricePerNight;
        }

        public RoomType RoomType { get; private set; }
        public decimal PricePerNight { get; private set; }
    }
}