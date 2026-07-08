using Microsoft.EntityFrameworkCore;
using YbsSmartCardSystem.Database.AppDbContextModels;
using YbsSmartCardSystem.Domain.Features.Card.Models;

namespace YbsSmartCardSystem.Domain.Features.Card;

public class CardService
{
    private readonly AppDbContext _db;

    public CardService(AppDbContext db)
    {
        _db = db;
    }

    public Result<CardListResponseModel> GetList(CardListRequestModel request)
    {
        try
        {
            int pageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            int pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var query = _db.TblCards
                .AsNoTracking()
                .Where(x => x.IsDelete == false);

            if (!string.IsNullOrWhiteSpace(request.CardNo))
            {
                string cardNo = request.CardNo.Trim();
                query = query.Where(x => x.CardNo.Contains(cardNo));
            }

            if (!string.IsNullOrWhiteSpace(request.MobileNo))
            {
                string mobileNo = request.MobileNo.Trim();
                query = query.Where(x => x.MobileNo.Contains(mobileNo));
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                string name = request.Name.Trim();
                query = query.Where(x => x.FullName.Contains(name));
            }

            var cards = query
                .OrderByDescending(x => x.CardId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Result<CardListResponseModel>
            {
                IsSuccess = true,
                Message = "Cards retrieved successfully.",
                Data = new CardListResponseModel
                {
                    Cards = cards.Select(c => new CardModel
                    {
                        CardId = c.CardId,
                        CardNo = c.CardNo,
                        FullName = c.FullName,
                        MobileNo = c.MobileNo,
                        Balance = c.Balance
                    }).ToList()
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<CardListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<CardModel> GetById(int cardId)
    {
        try
        {
            var card = _db.TblCards
                .AsNoTracking()
                .Where(x => x.CardId == cardId && x.IsDelete == false)
                .Select(x => new CardModel
                {
                    CardId = x.CardId,
                    CardNo = x.CardNo,
                    FullName = x.FullName,
                    MobileNo = x.MobileNo,
                    Balance = x.Balance
                })
                .FirstOrDefault();

            if (card is null)
            {
                return Fail("Card not found.");
            }

            return new Result<CardModel>
            {
                IsSuccess = true,
                Message = "Card retrieved successfully.",
                Data = card
            };
        }
        catch (Exception ex)
        {
            return new Result<CardModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<CardModel> Create(CardCreateRequestModel request)
    {
        try
        {
            string cardNo = request.CardNo.Trim();
            string fullName = request.FullName.Trim();
            string mobileNo = request.MobileNo.Trim();

            if (string.IsNullOrWhiteSpace(cardNo))
            {
                return Fail("Card No is required.");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                return Fail("Full Name is required.");
            }

            if (string.IsNullOrWhiteSpace(mobileNo))
            {
                return Fail("Mobile Number is required.");
            }

            if (_db.TblCards.Any(x => x.CardNo == cardNo))
            {
                return Fail("Card No already exists.");
            }

            var card = new TblCard
            {
                CardNo = cardNo,
                FullName = fullName,
                MobileNo = mobileNo,
                Balance = 0,
                CreatedDateTime = DateTime.Now,
                IsDelete = false
            };

            _db.TblCards.Add(card);
            _db.SaveChanges();

            return new Result<CardModel>
            {
                IsSuccess = true,
                Message = "Card created successfully.",
                Data = new CardModel
                {
                    CardId = card.CardId,
                    CardNo = card.CardNo,
                    FullName = card.FullName,
                    MobileNo = card.MobileNo,
                    Balance = card.Balance
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<CardModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static Result<CardModel> Fail(string message)
    {
        return new Result<CardModel>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
