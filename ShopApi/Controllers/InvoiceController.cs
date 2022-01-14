using Microsoft.AspNetCore.Mvc;
using ShopApi.Business.DTO;
using ShopApi.Business.UseCase;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : Controller
    {
        [HttpGet("GetAllInvoice")]
        public JsonResult GetAllInvoice()
        {
            using (InvoiceBo bo = new InvoiceBo())
            {
                var data = bo.GetAllInvoice();
                return Json(data);
            }
        }

        /// <summary>
        /// Fatura Hesaplama
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CalculationInvoice")]
        public JsonResult CalculationInvoice(InvoiceCalculationModel model)
        {
            using (InvoiceBo bo = new InvoiceBo())
            {
                var data = bo.CalculationInvoice(model);
                return Json(data);
            }
        }
    }
}
