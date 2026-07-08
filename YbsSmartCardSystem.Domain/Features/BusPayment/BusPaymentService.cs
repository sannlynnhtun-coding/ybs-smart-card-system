using Microsoft.EntityFrameworkCore;
using YbsSmartCardSystem.Database.AppDbContextModels;
using YbsSmartCardSystem.Domain.Features.BusPayment.Models;

namespace YbsSmartCardSystem.Domain.Features.BusPayment;

public class BusPaymentService
{
    private readonly AppDbContext _db;
    private readonly decimal _fareAmount;

    public BusPaymentService(AppDbContext db, decimal fareAmount)
    {
        _db = db;
        _fareAmount = fareAmount;
    }

    public Result<BusTapResponseModel> Tap(BusTapRequestModel request)
    {
        try
        {
            string cardNo = request.CardNo.Trim();
            if (string.IsNullOrWhiteSpace(cardNo))
            {
                return Fail("Card No is required.");
            }

            var card = _db.TblCards.FirstOrDefault(x => x.CardNo == cardNo && x.IsDelete == false);
            if (card is null)
            {
                return Fail("Card not found.");
            }

            if (card.Balance < _fareAmount)
            {
                return Fail("Insufficient Balance");
            }

            using var dbTransaction = _db.Database.BeginTransaction();

            card.Balance -= _fareAmount;
            card.ModifiedDateTime = DateTime.Now;

            _db.TblTransactions.Add(new TblTransaction
            {
                TransactionNo = $"TXN-{Guid.NewGuid():N}",
                CardId = card.CardId,
                Amount = -_fareAmount,
                CreatedDateTime = DateTime.Now,
                IsDelete = false
            });

            _db.SaveChanges();
            dbTransaction.Commit();

            return new Result<BusTapResponseModel>
            {
                IsSuccess = true,
                Message = "Boarding allowed.",
                Data = new BusTapResponseModel
                {
                    CardNo = card.CardNo,
                    FareAmount = _fareAmount,
                    NewBalance = card.Balance,
                    IsAllowed = true
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<BusTapResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static Result<BusTapResponseModel> Fail(string message)
    {
        return new Result<BusTapResponseModel>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
