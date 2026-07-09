namespace YbsSmartCardSystem.Domain.Features.Card.Models;

public class CardListRequestModel
{
    public string? CardNo { get; set; }

    public string? MobileNo { get; set; }

    public string? Name { get; set; }

    public int PageNo { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class CardListResponseModel
{
    public List<CardListItemResponseModel> Cards { get; set; } = new List<CardListItemResponseModel>();

    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int PageCount { get; set; }
}

public class CardListItemResponseModel
{
    public int CardId { get; set; }

    public string CardNo { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}

public class CardDetailRequestModel
{
    public int CardId { get; set; }
}

public class CardDetailResponseModel
{
    public int CardId { get; set; }

    public string CardNo { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}

public class CardCreateRequestModel
{
    public string CardNo { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;
}

public class CardCreateResponseModel
{
    public int CardId { get; set; }

    public string CardNo { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;
}
