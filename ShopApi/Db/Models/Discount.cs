using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Db.Models
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public int DiscountRate { get; set; }
        public string DiscountType { get; set; }

    }
}
