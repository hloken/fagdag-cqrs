using TechTalk.SpecFlow;

namespace FagdagCqrs.Specs.Steps
{
    [Binding]
    public class BookingThens
    {
        [Then(@"skal jeg se totalprisen før bestillingen bekreftes")]
        public void SaSkalJegSeTotalprisenForBestillingenBekreftes()
        {
            ScenarioContext.Current.Pending();
        }
 
    }
}