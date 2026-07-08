namespace YbsSmartCardSystem.Domain.Features.Topup.Models;

public class TopupRequestModel
{
    public string CardNo { get; set; } = string.Empty;

    public int PackageId { get; set; }
}

public class TopupResponseModel
{
    public string CardNo { get; set; } = string.Empty;

    public string PackageName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public decimal NewBalance { get; set; }
}
