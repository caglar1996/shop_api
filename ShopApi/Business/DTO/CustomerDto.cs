using System;

namespace ShopApi.Business.DTO
{
    public class CustomerDto
    {
        /// <summary>
        /// TC No
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsShopEmployee { get; set; }
        public bool IsConnectShop { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
