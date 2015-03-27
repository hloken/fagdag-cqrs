using System;
using FagdagCqrs.Specs.Arguments;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Drivers
{
    public class BookingGuiDriver
    {
        private IWebDriver _webDriver;

        public BookingGuiDriver(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void MakeBooking(RomType romType, DateTime fraDato, int lengdePåOpphold)
        {
            //_webDriver.Navigate<
        }
    }
}