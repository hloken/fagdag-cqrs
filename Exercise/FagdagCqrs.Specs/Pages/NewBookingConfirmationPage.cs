using System;
using FagdagCqrs.Specs.AngularBindingAdapters;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Pages
{
    public class NewBookingConfirmationPage : Page
    {
        private readonly Guid _bookingId;

        public NewBookingConfirmationPage(IWebDriver webDriver, Guid bookingId) : base(webDriver)
        {
            _bookingId = bookingId;
        }

        protected override string Url
        {
            get { return string.Format("#/booking/{0}/confirmation", _bookingId); }
        }

        public AngularTextBinding TotalPrice
        {
            get
            {
                return AngularView.TextBinding("booking.price");
            }
        }
    }
}