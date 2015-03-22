using System;
using Services.Cruft;

namespace Services
{
    public interface ICustomerService
    {
        void MakeCustomerPreferred(Guid customerId);
        ICustomer GetCustomer(Guid customerId);
        ICustomerSet GetCustomersWithName(string name);
        ICustomerSet GetPreferredCustomers();
        void ChangeCustomerLocale(Guid customerId, Locale newLocale);
        void CreateCustomer(ICustomer customer);
        void EditCustomerDetails(ICustomerDetails customerDetails);
    }
};