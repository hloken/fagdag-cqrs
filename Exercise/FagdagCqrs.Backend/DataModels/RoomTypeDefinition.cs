using FagdagCqrs.Backend.Contracts;

namespace FagdagCqrs.Backend.DataModels
{
    public class RoomTypeDefinition
    {
        public RoomTypeDefinition(RoomType roomType, decimal pricePerNight)
        {
            RoomType = roomType;
            PricePerNight = pricePerNight;
        }

        public RoomType RoomType { get; private set; }
        public decimal PricePerNight { get; private set; }
    }
}