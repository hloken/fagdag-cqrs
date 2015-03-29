using System;
using System.Collections.Generic;
using System.Linq;
using FagdagCqrs.Specs.Arguments;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Pages
{
    public class BookingsPage : Page
    {
        public BookingsPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        protected override string Url
        {
            get { return "#/booking"; }
        }

        private IEnumerable<IWebElement> BookingTableRows
        {
            get { return WebDriver.FindElements(By.CssSelector("div.booking-info")); }
        }

        public IEnumerable<RomReservasjon> GetBookings()
        {
            WaitUntilLoaded();

            return BookingTableRows.Select(MapFromRow);
        }

        public RomReservasjon MapFromRow(IWebElement rowElement)
        {
            var romType = (RomType) Enum.Parse(typeof (RomType), rowElement.FindElement(By.Id("roomTypeName")).Text);
            var fraDato = DateTime.Parse(rowElement.FindElement(By.Id("fromDate")).Text);
            var lengdePåOpphold = int.Parse(rowElement.FindElement(By.Id("duration")).Text);

            return new RomReservasjon
            {
                RomType = romType,
                FraDato = fraDato,
                LengdePåOpphold = lengdePåOpphold
            };
        }
    }
}