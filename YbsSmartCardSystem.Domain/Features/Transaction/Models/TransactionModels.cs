namespace YbsSmartCardSystem.Domain.Features.Transaction.Models;

public class TransactionListRequestModel
{
    public string CardNo { get; set; } = string.Empty;
}

public class TransactionListResponseModel
{
    public List<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
}

public class TransactionDetailRequestModel
{
    public int TransactionId { get; set; }
}

public class TransactionDetailResponseModel
{
    public TransactionDetailModel Transaction { get; set; } = new TransactionDetailModel();
}

public class TransactionModel
{
    public string TransactionNo { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfterTransaction { get; set; }
}

public class TransactionDetailModel
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
