namespace YbsSmartCardSystem.Domain.Features.Package.Models;

public class PackageRequestModel
{
    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageListResponseModel
{
    public List<PackageModel> Packages { get; set; } = new List<PackageModel>();
}

public class PackageModel
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}
