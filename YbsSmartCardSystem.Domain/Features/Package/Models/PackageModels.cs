namespace YbsSmartCardSystem.Domain.Features.Package.Models;

public class PackageListRequestModel
{
}

public class PackageListResponseModel
{
    public List<PackageListItemResponseModel> Packages { get; set; } = new List<PackageListItemResponseModel>();
}

public class PackageListItemResponseModel
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageDetailRequestModel
{
    public int PackageId { get; set; }
}

public class PackageDetailResponseModel
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageCreateRequestModel
{
    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageCreateResponseModel
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageUpdateRequestModel
{
    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageUpdateResponseModel
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}

public class PackageDeleteRequestModel
{
    public int PackageId { get; set; }
}

public class PackageDeleteResponseModel
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}
