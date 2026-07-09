namespace YbsSmartCardSystem.Domain.Features.Transaction.Models;

public class TransactionListRequestModel
{
    public string CardNo { get; set; } = string.Empty;

    public int PageNo { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public class TransactionListResponseModel
{
    public List<TransactionListItemResponseModel> Transactions { get; set; } = new List<TransactionListItemResponseModel>();

    public int PageNo { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int PageCount { get; set; }
}

public class TransactionDetailRequestModel
{
    public int TransactionId { get; set; }
}

public class TransactionDetailResponseModel
{
    public TransactionDetailItemResponseModel Transaction { get; set; } = new TransactionDetailItemResponseModel();
}

public class TransactionListItemResponseModel
{
    public int TransactionId { get; set; }

    public string TransactionNo { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfterTransaction { get; set; }
}

public class TransactionDetailItemResponseModel
{
    public int TransactionId { get; set; }

    public string TransactionNo { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfterTransaction { get; set; }

    public string CardNo { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string MobileNo { get; set; } = string.Empty;
}
