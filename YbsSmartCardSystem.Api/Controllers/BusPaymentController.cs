using Microsoft.AspNetCore.Mvc;
using YbsSmartCardSystem.Domain.Features.BusPayment;
using YbsSmartCardSystem.Domain.Features.BusPayment.Models;

namespace YbsSmartCardSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusPaymentController : BaseController
{
    private readonly BusPaymentService _busPaymentService;

    public BusPaymentController(BusPaymentService busPaymentService)
    {
        _busPaymentService = busPaymentService;
    }

    [HttpPost("tap")]
    public IActionResult Tap([FromBody] BusTapRequestModel request)
    {
        var result = _busPaymentService.Tap(request);
        return Execute(result);
    }
}
