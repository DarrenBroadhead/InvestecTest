using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using AnyCompany.Model;
using AnyCompany.Repository;
using AnyCompany.Repository.Interfaces;
using AnyCompany.Service;
using AnyCompany.Service.Interfaces;

namespace AnyCompany.UnitTest
{
    [TestClass]
    public class OrderUnitTest
    {
        private Customer _customer;
        private Order _order;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderUnitTest()
        {
            IOrderRepository orderRepository = new OrderRepository();
            _orderService = new OrderService(orderRepository);
            _customerService = new CustomerService();
        }

        [TestInitialize]
        public void Init()
        {
            _customer = new Customer
            {
                Name = $"Customer - {Guid.NewGuid()}",
                Country = "SA",
                DateOfBirth = DateTime.Now
            };

            _order = new Order
            {
                Amount = 150.43
            };
        }

        [TestMethod]
        public void PlaceZeroAmountOrderTest()
        {
            Customer customer = GetCustomer(_customer.Country);

            if (customer == null) return;

            _order.Amount = 0;

            var result = _orderService.PlaceOrder(_order, customer.CustomerId);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PlaceNonUkOrderTest()
        {
            Customer customer = GetCustomer(_customer.Country);

            if (customer == null) return;

            var result = _orderService.PlaceOrder(_order, customer.CustomerId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Order));
            Assert.IsTrue(result.OrderId > 0);
        }

        [TestMethod]
        public void PlaceUkOrderTest()
        {
            Customer customer = GetCustomer("UK");

            if (customer == null) return;

            var result = _orderService.PlaceOrder(_order, customer.CustomerId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Order));
            Assert.IsTrue(result.OrderId > 0);
        }

        private Customer GetCustomer(string country = null)
        {
            var customers = _customerService.LoadAll().Where(e => e.Country.ToUpper() == country?.ToUpper()).ToList();

            Customer customer;

            if (customers.Any())
                customer = customers.FirstOrDefault();
            else
            {
                _customer.Country = country;
                customer = _customerService.Save(_customer);
            }

            return customer;
        }
    }
}