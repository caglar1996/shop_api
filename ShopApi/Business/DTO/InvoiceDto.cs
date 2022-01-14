using System;

namespace ShopApi.Business.DTO
{
    public class InvoiceDto
    {
        public int CustomerId { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public double DiscountRate { get; set; }
        public double NetPrice { get; set; }
        public bool IsShoppingAction { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
