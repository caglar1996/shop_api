namespace ShopApi.Business.DTO
{
    public class InvoiceCalculationModel
    {
        public int CustomerId { get; set; }
        public bool IsShopping { get; set; }
        public double Price { get; set; }
    }
}
