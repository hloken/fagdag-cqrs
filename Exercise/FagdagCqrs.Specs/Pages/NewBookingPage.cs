using FagdagCqrs.Specs.AngularBindingAdapters;
using OpenQA.Selenium;

namespace FagdagCqrs.Specs.Pages
{
    public class NewBookingPage : Page
    {
        public NewBookingPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        protected override string Url
        {
            get { return "#/booking/new"; }
        }

        public AngularSelectBinding RoomType { get { return AngularView.SelectBinding("newBooking.roomType"); } }
        public AngularInputBinding FromDate { get { return AngularView.InputBinding("newBooking.fromDate"); } }
        public AngularInputBinding Duration { get { return AngularView.InputBinding("newBooking.duration"); } }
        public AngularClickBinding SaveButton { get { return AngularView.ClickBinding("save()"); } }
    }
}