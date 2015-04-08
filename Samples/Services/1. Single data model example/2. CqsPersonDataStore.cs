using Services.Cruft;

namespace Services
{
    // Simple CQS Example
    public class CqsPersonDataStore
    {

        // Query Method
        public Person GetPerson(int id)
        {
            // query data storage for specific Person by Id
            // return Person
        }

        // Command Methods
        public void Insert(Person p)
        {
            // Insert Person into data storage
        }

        public void UpdateName(int id, string name)
        {
            // Find Person in data storage by Id
            // Update the name for this Person within the data storage
        }
    }
}