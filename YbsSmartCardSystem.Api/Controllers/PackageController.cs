using Microsoft.AspNetCore.Mvc;
using YbsSmartCardSystem.Domain.Features.Package;
using YbsSmartCardSystem.Domain.Features.Package.Models;

namespace YbsSmartCardSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackageController : BaseController
{
    private readonly PackageService _packageService;

    public PackageController(PackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet]
    public IActionResult PackageList()
    {
        var result = _packageService.GetList();
        return Execute(result);
    }

    [HttpPost]
    public IActionResult CreatePackage([FromBody] PackageRequestModel request)
    {
        var result = _packageService.Create(request);
        return Execute(result);
    }

    [HttpPut("{packageId}")]
    public IActionResult UpdatePackage(int packageId, [FromBody] PackageRequestModel request)
    {
        var result = _packageService.Update(packageId, request);
        return Execute(result);
    }
}
