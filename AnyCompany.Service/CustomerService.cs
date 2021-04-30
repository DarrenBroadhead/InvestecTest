using System.Collections.Generic;
using AnyCompany.Model;
using AnyCompany.Repository;
using AnyCompany.Service.Interfaces;

namespace AnyCompany.Service
{
    public class CustomerService : ICustomerService
    {
        public Customer Load(int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            return customer;
        }

        public List<Customer> LoadAll()
        {
            List<Customer> customers = CustomerRepository.LoadAll();

            return customers;
        }

        public Customer Save(Customer customer)
        {
            var result = CustomerRepository.Save(customer);

            return result;
        }
    }
}