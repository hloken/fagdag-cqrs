using System;
using Services.Cruft;

namespace Services.Cqrs
{
    public interface ICustomerReadService
    {
        ICustomer GetCustomer(Guid customerId);
        ICustomerSet GetCustomersWithName(string name);
        ICustomerSet GetPreferredCustomers();
    }
}