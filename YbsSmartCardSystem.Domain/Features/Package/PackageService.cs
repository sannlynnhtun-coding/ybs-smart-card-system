using Microsoft.EntityFrameworkCore;
using YbsSmartCardSystem.Database.AppDbContextModels;
using YbsSmartCardSystem.Domain.Features.Package.Models;

namespace YbsSmartCardSystem.Domain.Features.Package;

public class PackageService
{
    private readonly AppDbContext _db;

    public PackageService(AppDbContext db)
    {
        _db = db;
    }

    public Result<PackageListResponseModel> GetList(PackageListRequestModel request)
    {
        try
        {
            int pageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            int pageSize = Math.Min(request.PageSize <= 0 ? 10 : request.PageSize, 100);

            var query = _db.TblPackages
                .AsNoTracking()
                .Where(x => x.IsDelete == false);

            int totalCount = query.Count();
            var packages = query
                .OrderByDescending(x => x.PackageId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PackageListItemResponseModel
                {
                    PackageId = x.PackageId,
                    PackageName = x.PackageName,
                    Amount = x.Amount
                })
                .ToList();

            return new Result<PackageListResponseModel>
            {
                IsSuccess = true,
                Message = "Packages retrieved successfully.",
                Data = new PackageListResponseModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    PageCount = (int)Math.Ceiling(totalCount / (double)pageSize),
                    Packages = packages
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<PackageDetailResponseModel> GetById(PackageDetailRequestModel request)
    {
        try
        {
            var package = _db.TblPackages
                .AsNoTracking()
                .Where(x => x.PackageId == request.PackageId && x.IsDelete == false)
                .Select(x => new PackageDetailResponseModel
                {
                    PackageId = x.PackageId,
                    PackageName = x.PackageName,
                    Amount = x.Amount
                })
                .FirstOrDefault();

            if (package is null)
            {
                return Fail<PackageDetailResponseModel>("Package not found.");
            }

            return new Result<PackageDetailResponseModel>
            {
                IsSuccess = true,
                Message = "Package retrieved successfully.",
                Data = package
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageDetailResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<PackageCreateResponseModel> Create(PackageCreateRequestModel request)
    {
        try
        {
            string packageName = request.PackageName.Trim();
            if (string.IsNullOrWhiteSpace(packageName))
            {
                return Fail<PackageCreateResponseModel>("Package Name is required.");
            }

            if (request.Amount <= 0)
            {
                return Fail<PackageCreateResponseModel>("Amount must be greater than 0.");
            }

            var package = new TblPackage
            {
                PackageName = packageName,
                Amount = request.Amount,
                CreatedDateTime = DateTime.Now,
                IsDelete = false
            };

            _db.TblPackages.Add(package);
            _db.SaveChanges();

            return new Result<PackageCreateResponseModel>
            {
                IsSuccess = true,
                Message = "Package created successfully.",
                Data = new PackageCreateResponseModel
                {
                    PackageId = package.PackageId,
                    PackageName = package.PackageName,
                    Amount = package.Amount
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageCreateResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<PackageUpdateResponseModel> Update(PackageUpdateRequestModel request)
    {
        try
        {
            string packageName = request.PackageName.Trim();
            if (string.IsNullOrWhiteSpace(packageName))
            {
                return Fail<PackageUpdateResponseModel>("Package Name is required.");
            }

            if (request.Amount <= 0)
            {
                return Fail<PackageUpdateResponseModel>("Amount must be greater than 0.");
            }

            var package = _db.TblPackages.FirstOrDefault(x => x.PackageId == request.PackageId && x.IsDelete == false);
            if (package is null)
            {
                return Fail<PackageUpdateResponseModel>("Package not found.");
            }

            package.PackageName = packageName;
            package.Amount = request.Amount;
            package.ModifiedDateTime = DateTime.Now;

            _db.SaveChanges();

            return new Result<PackageUpdateResponseModel>
            {
                IsSuccess = true,
                Message = "Package updated successfully.",
                Data = new PackageUpdateResponseModel
                {
                    PackageId = package.PackageId,
                    PackageName = package.PackageName,
                    Amount = package.Amount
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageUpdateResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<PackageDeleteResponseModel> Delete(PackageDeleteRequestModel request)
    {
        try
        {
            var package = _db.TblPackages.FirstOrDefault(x => x.PackageId == request.PackageId && x.IsDelete == false);
            if (package is null)
            {
                return Fail<PackageDeleteResponseModel>("Package not found.");
            }

            package.IsDelete = true;
            package.ModifiedDateTime = DateTime.Now;

            _db.SaveChanges();

            return new Result<PackageDeleteResponseModel>
            {
                IsSuccess = true,
                Message = "Package deleted successfully.",
                Data = new PackageDeleteResponseModel
                {
                    PackageId = package.PackageId,
                    PackageName = package.PackageName,
                    Amount = package.Amount
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageDeleteResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static Result<T> Fail<T>(string message)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
