using FagdagCqrs.Specs.Arguments;
using FagdagCqrs.Specs.Drivers;
using TechTalk.SpecFlow;

namespace FagdagCqrs.Specs.Steps
{
    [Binding]
    public class BookingWhens
    {
        private readonly BookingGuiDriver _bookingGuiDriver;

        public BookingWhens(BookingGuiDriver bookingGuiDriver)
        {
            _bookingGuiDriver = bookingGuiDriver;
        }

        [When(@"jeg reserverer et rom")]
        public void NarJegReservererEtRom(RomReservasjon romReservasjon)
        {
            _bookingGuiDriver.MakeBooking(romReservasjon.RomType, romReservasjon.FraDato, romReservasjon.LengdePåOpphold);
        }

    }
}
