using System.Globalization;
using FagdagCqrs.Specs.Arguments;
using FagdagCqrs.Specs.Drivers;
using FagdagCqrs.Specs.Helpers;
using FagdagCqrs.Specs.Pages;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace FagdagCqrs.Specs.Steps
{
    [Binding]
    public class BookingThens
    {
        private readonly BookingApiDriver _bookingApiDriver;
        private readonly IWebDriver _webDriver;

        public BookingThens(WebDriverInstanceWrapper webDriverInstanceWrapper, BookingApiDriver bookingApiDriver)
        {
            _bookingApiDriver = bookingApiDriver;
            _webDriver = webDriverInstanceWrapper.Instance;
        }

        [Then(@"skal jeg se totalprisen '(.*)' før bestillingen bekreftes for reservasjon")]
        public void SaSkalJegSeTotalprisenForBestillingenBekreftesForReservasjon(decimal totalPris, RomReservasjon romReservasjon)
        {
            var booking = _bookingApiDriver.FindBookingBy(romReservasjon.RomType, romReservasjon.FraDato, romReservasjon.LengdePåOpphold);

            var page = _webDriver.Navigate<NewBookingConfirmationPage>(booking.Id);

            page.TotalPrice.Value.Should().Be(totalPris.ToString(CultureInfo.InvariantCulture));
        }


        [Then(@"skal jeg se reservasjonen i reservasjonslisten")]
        public void SaSkalJegSeReservasjonenIReservasjonsListen(RomReservasjon romReservasjon)
        {
            var page = _webDriver.NavigateAndRefresh<BookingsPage>();
            var actualListBookings = page.GetBookings();
           
            actualListBookings.Should().Contain( booking => 
                booking.RomType == romReservasjon.RomType &&
                booking.FraDato == romReservasjon.FraDato &&
                booking.LengdePåOpphold == romReservasjon.LengdePåOpphold);
        }

    }
}