using Microsoft.AspNetCore.Mvc;
using ShopApi.Business.DTO;
using ShopApi.Business.UseCase;
using ShopApi.Core.UseCase.Bussines;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {

        [HttpGet("GetAllCustomer")]
        public JsonResult GetAllCustomer()
        {
            using (CustomerBo bo = new CustomerBo())
            {
                var data = bo.GetAllCustomer();
                return Json(data);
            }
        }

        [HttpGet("GetDiscountType")]
        public JsonResult GetDiscountType(int customerId)
        {
            using (DiscountBo bo = new DiscountBo())
            {
                var data = bo.GetDiscountModel(customerId);
                return Json(data);
            }
        }



        [HttpPost("AddCustomer")]
        public JsonResult AddCustomer(CustomerDto customer)
        {
            using (CustomerBo bo = new CustomerBo())
            {
                var data = bo.AddCustomer(customer);
                return Json(data);
            }
        }



    }
}
