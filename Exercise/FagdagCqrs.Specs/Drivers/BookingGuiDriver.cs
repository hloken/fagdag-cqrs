using System;
using FagdagCqrs.Specs.Arguments;
using FagdagCqrs.Specs.Helpers;
using FagdagCqrs.Specs.Pages;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Drivers
{
    public class BookingGuiDriver
    {
        private readonly IWebDriver _webDriver;

        public BookingGuiDriver(WebDriverInstanceWrapper client)
        {
            _webDriver = client.Instance;
        }

        public void MakeBooking(RomType romType, DateTime fraDato, int lengdePåOpphold)
        {
            var page = _webDriver.Navigate<NewBookingPage>();
            page.WaitUntilLoaded();

            page.RoomType.SelectFirstOptionThatContainsText(romType.ToString());
            page.FromDate.TypeDate(fraDato);
            page.Duration.TypeText(lengdePåOpphold);

            page.SaveButton.Click();
        }
    }
}