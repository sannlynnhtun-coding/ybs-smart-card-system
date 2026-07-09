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
            int pageSize = Math.Min(request.PageSize <= 0 ? 10 : request.PageSize, 100);

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

            int totalCount = query.Count();
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
                    PageNo = pageNo,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    PageCount = (int)Math.Ceiling(totalCount / (double)pageSize),
                    Cards = cards.Select(c => new CardListItemResponseModel
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

    public Result<CardDetailResponseModel> GetById(CardDetailRequestModel request)
    {
        try
        {
            var card = _db.TblCards
                .AsNoTracking()
                .Where(x => x.CardId == request.CardId && x.IsDelete == false)
                .Select(x => new CardDetailResponseModel
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
                return CardDetailFail("Card not found.");
            }

            return new Result<CardDetailResponseModel>
            {
                IsSuccess = true,
                Message = "Card retrieved successfully.",
                Data = card
            };
        }
        catch (Exception ex)
        {
            return new Result<CardDetailResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<CardCreateResponseModel> Create(CardCreateRequestModel request)
    {
        try
        {
            string cardNo = request.CardNo.Trim();
            string fullName = request.FullName.Trim();
            string mobileNo = request.MobileNo.Trim();

            if (string.IsNullOrWhiteSpace(cardNo))
            {
                return CardCreateFail("Card No is required.");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                return CardCreateFail("Full Name is required.");
            }

            if (string.IsNullOrWhiteSpace(mobileNo))
            {
                return CardCreateFail("Mobile Number is required.");
            }

            if (_db.TblCards.Any(x => x.CardNo == cardNo))
            {
                return CardCreateFail("Card No already exists.");
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

            return new Result<CardCreateResponseModel>
            {
                IsSuccess = true,
                Message = "Card created successfully.",
                Data = new CardCreateResponseModel
                {
                    CardId = card.CardId,
                    CardNo = card.CardNo,
                    FullName = card.FullName,
                    MobileNo = card.MobileNo,
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<CardCreateResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static Result<CardDetailResponseModel> CardDetailFail(string message)
    {
        return new Result<CardDetailResponseModel>
        {
            IsSuccess = false,
            Message = message
        };
    }

    private static Result<CardCreateResponseModel> CardCreateFail(string message)
    {
        return new Result<CardCreateResponseModel>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
