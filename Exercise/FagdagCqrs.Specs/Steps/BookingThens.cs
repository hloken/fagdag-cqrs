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
        private readonly IWebDriver _webDriver;

        public BookingThens(WebDriverInstanceWrapper webDriverInstanceWrapper)
        {
            _webDriver = webDriverInstanceWrapper.Instance;
        }

        [Then(@"skal jeg se totalprisen før bestillingen bekreftes")]
        public void SaSkalJegSeTotalprisenForBestillingenBekreftes()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"skal jeg se reservasjonen i reservasjons listen")]
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