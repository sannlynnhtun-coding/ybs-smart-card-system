namespace YbsSmartCardSystem.Domain.Features.Transaction.Models;

public class TransactionListRequestModel
{
    public string CardNo { get; set; } = string.Empty;
}

public class TransactionListResponseModel
{
    public List<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
}

public class TransactionModel
{
    public string TransactionNo { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfterTransaction { get; set; }
}
