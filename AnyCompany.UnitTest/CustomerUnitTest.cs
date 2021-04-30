using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using AnyCompany.Model;
using AnyCompany.Service;
using AnyCompany.Service.Interfaces;

namespace AnyCompany.UnitTest
{
    [TestClass]
    public class CustomerUnitTest
    {
        private readonly ICustomerService _customerService;

        public CustomerUnitTest()
        {
            _customerService = new CustomerService();
        }

        [TestMethod]
        public void LoadAllTest()
        {
            List<Customer> customers = _customerService.LoadAll();

            Assert.IsNotNull(customers);
            Assert.IsNotNull(customers.FirstOrDefault()?.Orders);
            Assert.IsTrue(customers.Count > 0);
            Assert.IsTrue(customers.FirstOrDefault()?.Orders.Count > 0);
            Assert.IsInstanceOfType(customers, typeof(List<Customer>));
            Assert.IsInstanceOfType(customers.FirstOrDefault()?.Orders, typeof(List<Order>));
        }
    }
}