namespace YbsSmartCardSystem.Domain.Features.Transaction.Models;

public class TransactionListRequestModel
{
    public string CardNo { get; set; } = string.Empty;
}

public class TransactionListResponseModel
{
    public List<TransactionListItemResponseModel> Transactions { get; set; } = new List<TransactionListItemResponseModel>();
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
