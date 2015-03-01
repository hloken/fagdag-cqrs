using System;
using Services.Cruft;

namespace Services.Cqrs
{
    public interface ICustomerWriteService
    {
        void MakeCustomerPreferred(ICustomer customerId);
        void ChangeCustomerLocale(Guid customerId, Locale newLocale);
        void CreateCustomer(ICustomer customer);
        void EditCustomerDetails(ICustomerDetails customerDetails);
    }
}