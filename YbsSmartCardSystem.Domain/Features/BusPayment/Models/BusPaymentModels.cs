namespace YbsSmartCardSystem.Domain.Features.BusPayment.Models;

public class BusTapRequestModel
{
    public string CardNo { get; set; } = string.Empty;
}

public class BusTapResponseModel
{
    public string CardNo { get; set; } = string.Empty;

    public decimal FareAmount { get; set; }

    public decimal NewBalance { get; set; }

    public bool IsAllowed { get; set; }
}
