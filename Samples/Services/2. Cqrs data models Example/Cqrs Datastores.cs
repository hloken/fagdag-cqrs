using Services.Cruft;

namespace Services
{
    // Responsibility Segregation Example
    public class QueryDataStore
    {
        public Person GetPerson(int id)
        {
            // query data storage for specific Person by Id
            // return Person
        }
    }

    public class CommandDataStore
    {
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