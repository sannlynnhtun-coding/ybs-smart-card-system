using Microsoft.AspNetCore.Mvc;
using YbsSmartCardSystem.Domain.Features.Topup;
using YbsSmartCardSystem.Domain.Features.Topup.Models;

namespace YbsSmartCardSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopupController : BaseController
{
    private readonly TopupService _topupService;

    public TopupController(TopupService topupService)
    {
        _topupService = topupService;
    }

    [HttpPost]
    public IActionResult Topup([FromBody] TopupRequestModel request)
    {
        var result = _topupService.Topup(request);
        return Execute(result);
    }
}
