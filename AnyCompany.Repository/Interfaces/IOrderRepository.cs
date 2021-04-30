using System.Collections.Generic;
using AnyCompany.Model;

namespace AnyCompany.Repository.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadAll();
        Order Save(Order order);
    }
}