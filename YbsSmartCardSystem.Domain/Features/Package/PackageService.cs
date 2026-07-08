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

    public Result<PackageListResponseModel> GetList()
    {
        try
        {
            var packages = _db.TblPackages
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.PackageId)
                .Select(x => new PackageModel
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

    public Result<PackageModel> Create(PackageRequestModel request)
    {
        try
        {
            string packageName = request.PackageName.Trim();
            if (string.IsNullOrWhiteSpace(packageName))
            {
                return Fail("Package Name is required.");
            }

            if (request.Amount <= 0)
            {
                return Fail("Amount must be greater than 0.");
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

            return new Result<PackageModel>
            {
                IsSuccess = true,
                Message = "Package created successfully.",
                Data = ToModel(package)
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<PackageModel> Update(int packageId, PackageRequestModel request)
    {
        try
        {
            string packageName = request.PackageName.Trim();
            if (string.IsNullOrWhiteSpace(packageName))
            {
                return Fail("Package Name is required.");
            }

            if (request.Amount <= 0)
            {
                return Fail("Amount must be greater than 0.");
            }

            var package = _db.TblPackages.FirstOrDefault(x => x.PackageId == packageId && x.IsDelete == false);
            if (package is null)
            {
                return Fail("Package not found.");
            }

            package.PackageName = packageName;
            package.Amount = request.Amount;
            package.ModifiedDateTime = DateTime.Now;

            _db.SaveChanges();

            return new Result<PackageModel>
            {
                IsSuccess = true,
                Message = "Package updated successfully.",
                Data = ToModel(package)
            };
        }
        catch (Exception ex)
        {
            return new Result<PackageModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    private static PackageModel ToModel(TblPackage package)
    {
        return new PackageModel
        {
            PackageId = package.PackageId,
            PackageName = package.PackageName,
            Amount = package.Amount
        };
    }

    private static Result<PackageModel> Fail(string message)
    {
        return new Result<PackageModel>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
