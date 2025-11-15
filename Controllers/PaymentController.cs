using Harmic.Models;
using Harmic.Services.Momo;
using Microsoft.AspNetCore.Mvc;

namespace Harmic.Controllers
{
    public class PaymentController : Controller
    {

        private IMomoService _momoService;

        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }
        [HttpPost]
        public async Task<IActionResult> PaymentMomo(OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentMomo(model);
            return Redirect(response.PayUrl);
        }

        public IActionResult PaymentCallBack() {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }
    }
}
