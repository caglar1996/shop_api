using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Db.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Surname { get; set; }
        public int IsShopEmployee { get; set; }
        public int IsConnectShop { get; set; }
        public string InsertDate { get; set; }

    }
}
