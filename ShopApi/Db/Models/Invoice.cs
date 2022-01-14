using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Db.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public double DiscountRate { get; set; }
        public double NetPrice { get; set; }
        public int IsShoppingAction { get; set; }
        public string InsertDate { get; set; }
    }
}
