using Services.Cruft;

namespace Services
{
    // Simple CRUD Example
    public class CrudPersonDataStore
    {
        public Person Get(int id)
        {
            // query data storage for specific Person by Id
            // return Person
        }

        public void Insert(Person p)
        {
            // Insert Person into data storage
        }

        public void Update(Person p)
        {
            // Find Person in data storage by Id
            // Update the person within the data storage
        }

        public void Delete(int id)
        {
            // Delete the person from the data storage
        }
    }
};