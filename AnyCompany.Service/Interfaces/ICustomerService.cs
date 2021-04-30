using System.Collections.Generic;
using AnyCompany.Model;

namespace AnyCompany.Service.Interfaces
{
    public interface ICustomerService
    {
        Customer Load(int customerId);
        List<Customer> LoadAll();
        Customer Save(Customer customer);
    }
}