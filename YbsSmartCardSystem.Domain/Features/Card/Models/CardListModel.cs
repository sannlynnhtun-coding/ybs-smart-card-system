namespace YbsSmartCardSystem.Domain.Features.Card.Models;

public class CardListRequestModel
{
    public string? CardNo { get; set; }

    public string? MobileNo { get; set; }

    public string? Name { get; set; }

    public int PageNo { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class CardCreateRequestModel
{
    public string CardNo { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;
}

public class CardListResponseModel
{
    public List<CardModel> Cards { get; set; } = new List<CardModel>();
}

public class CardModel
{
    public int CardId { get; set; }

    public string CardNo { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }
}
