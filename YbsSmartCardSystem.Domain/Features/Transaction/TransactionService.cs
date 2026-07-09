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
                return new TransactionListItemResponseModel
                {
                    TransactionId = x.TransactionId,
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

    public Result<TransactionDetailResponseModel> GetById(TransactionDetailRequestModel request)
    {
        try
        {
            var transaction = _db.TblTransactions
                .AsNoTracking()
                .Include(x => x.Card)
                .FirstOrDefault(x =>
                    x.TransactionId == request.TransactionId &&
                    x.IsDelete == false &&
                    x.Card.IsDelete == false);

            if (transaction is null)
            {
                return new Result<TransactionDetailResponseModel>
                {
                    IsSuccess = false,
                    Message = "Transaction not found."
                };
            }

            var transactions = _db.TblTransactions
                .AsNoTracking()
                .Where(x => x.CardId == transaction.CardId && x.IsDelete == false)
                .OrderBy(x => x.CreatedDateTime)
                .ThenBy(x => x.TransactionId)
                .ToList();

            decimal balanceAfterTransaction = 0;
            foreach (var item in transactions)
            {
                balanceAfterTransaction += item.Amount;
                if (item.TransactionId == transaction.TransactionId)
                {
                    break;
                }
            }

            return new Result<TransactionDetailResponseModel>
            {
                IsSuccess = true,
                Message = "Transaction retrieved successfully.",
                Data = new TransactionDetailResponseModel
                {
                    Transaction = new TransactionDetailItemResponseModel
                    {
                        TransactionId = transaction.TransactionId,
                        TransactionNo = transaction.TransactionNo,
                        Date = transaction.CreatedDateTime,
                        Amount = transaction.Amount,
                        BalanceAfterTransaction = balanceAfterTransaction,
                        CardNo = transaction.Card.CardNo,
                        FullName = transaction.Card.FullName,
                        MobileNo = transaction.Card.MobileNo
                    }
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<TransactionDetailResponseModel>
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
