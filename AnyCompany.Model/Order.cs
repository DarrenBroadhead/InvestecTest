namespace AnyCompany.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public double VAT { get; set; }
    }
}