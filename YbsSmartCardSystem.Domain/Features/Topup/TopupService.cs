using Microsoft.EntityFrameworkCore;
using YbsSmartCardSystem.Database.AppDbContextModels;
using YbsSmartCardSystem.Domain.Features.Topup.Models;

namespace YbsSmartCardSystem.Domain.Features.Topup;

public class TopupService
{
    private readonly AppDbContext _db;

    public TopupService(AppDbContext db)
    {
        _db = db;
    }

    public Result<TopupResponseModel> Topup(TopupRequestModel request)
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

            var package = _db.TblPackages.FirstOrDefault(x => x.PackageId == request.PackageId && x.IsDelete == false);
            if (package is null)
            {
                return Fail("Package not found.");
            }

            using var dbTransaction = _db.Database.BeginTransaction();

            card.Balance += package.Amount;
            card.ModifiedDateTime = DateTime.Now;

            _db.TblTopups.Add(new TblTopup
            {
                CardId = card.CardId,
                PackageId = package.PackageId,
                CreatedDateTime = DateTime.Now,
                IsDelete = false
            });

            _db.TblTransactions.Add(new TblTransaction
            {
                TransactionNo = $"TXN-{Guid.NewGuid():N}",
                CardId = card.CardId,
                Amount = package.Amount,
                CreatedDateTime = DateTime.Now,
                IsDelete = false
            });

            _db.SaveChanges();
            dbTransaction.Commit();

            return new Result<TopupResponseModel>
            {
                IsSuccess = true,
                Message = "Top-up successful.",
                Data = new TopupResponseModel
                {
                    CardNo = card.CardNo,
                    PackageName = package.PackageName,
                    Amount = package.Amount,
                    NewBalance = card.Balance
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<TopupResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static Result<TopupResponseModel> Fail(string message)
    {
        return new Result<TopupResponseModel>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
