using System;
using AnyCompany.Model;
using AnyCompany.Repository;
using AnyCompany.Repository.Interfaces;
using AnyCompany.Service.Interfaces;

namespace AnyCompany.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order PlaceOrder(Order order, int customerId)
        {
            Customer customer = CustomerRepository.Load(customerId);

            if (order.Amount == 0)
                return null;

            CalculateVatByCountry(customer, order);

            order.CustomerId = customerId;

            var result = _orderRepository.Save(order);
            return result;
        }

        private void CalculateVatByCountry(Customer customer, Order order)
        {
            switch (customer.Country.ToUpper())
            {
                case "UK":
                    order.VAT = order.Amount * 0.2;
                    break;
                default:
                    order.VAT = order.Amount * 0.14;
                    break;
            }
        }
    }
}