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
    public IActionResult PackageList([FromQuery] PackageListRequestModel request)
    {
        var result = _packageService.GetList(request);
        return Execute(result);
    }

    [HttpGet("{packageId}")]
    public IActionResult PackageDetail([FromRoute] PackageDetailRequestModel request)
    {
        var result = _packageService.GetById(request);
        return Execute(result);
    }

    [HttpPost]
    public IActionResult CreatePackage([FromBody] PackageCreateRequestModel request)
    {
        var result = _packageService.Create(request);
        return Execute(result);
    }

    [HttpPut("{packageId}")]
    public IActionResult UpdatePackage(int packageId, [FromBody] PackageUpdateRequestModel request)
    {
        var result = _packageService.Update(packageId, request);
        return Execute(result);
    }

    [HttpDelete("{packageId}")]
    public IActionResult DeletePackage([FromRoute] PackageDeleteRequestModel request)
    {
        var result = _packageService.Delete(request);
        return Execute(result);
    }
}
