using Microsoft.EntityFrameworkCore;
using YbsSmartCardSystem.Database.AppDbContextModels;
using YbsSmartCardSystem.Domain.Features.Transaction.Models;

namespace YbsSmartCardSystem.Domain.Features.Transaction;

public class TransactionService
{
    private readonly AppDbContext _db;

    public TransactionService(AppDbContext db)
    {
        _db = db;
    }

    public Result<TransactionListResponseModel> GetList(TransactionListRequestModel request)
    {
        try
        {
            string cardNo = request.CardNo.Trim();
            if (string.IsNullOrWhiteSpace(cardNo))
            {
                return Fail("Card No is required.");
            }

            var card = _db.TblCards
                .AsNoTracking()
                .FirstOrDefault(x => x.CardNo == cardNo && x.IsDelete == false);

            if (card is null)
            {
                return Fail("Card not found.");
            }

            var transactions = _db.TblTransactions
                .AsNoTracking()
                .Where(x => x.CardId == card.CardId && x.IsDelete == false)
                .OrderBy(x => x.CreatedDateTime)
                .ThenBy(x => x.TransactionId)
                .ToList();

            decimal balance = 0;
            var items = transactions.Select(x =>
            {
                balance += x.Amount;
                return new TransactionModel
                {
                    TransactionNo = x.TransactionNo,
                    Date = x.CreatedDateTime,
                    Amount = x.Amount,
                    BalanceAfterTransaction = balance
                };
            }).ToList();

            return new Result<TransactionListResponseModel>
            {
                IsSuccess = true,
                Message = "Transactions retrieved successfully.",
                Data = new TransactionListResponseModel
                {
                    Transactions = items
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<TransactionListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static Result<TransactionListResponseModel> Fail(string message)
    {
        return new Result<TransactionListResponseModel>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
