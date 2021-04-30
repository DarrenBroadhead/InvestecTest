using AnyCompany.Model;

namespace AnyCompany.Service.Interfaces
{
    public interface IOrderService
    {
        Order PlaceOrder(Order order, int customerId);
    }
}