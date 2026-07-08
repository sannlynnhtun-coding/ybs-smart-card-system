using System;
using System.Collections.Generic;

namespace YbsSmartCardSystem.Database.AppDbContextModels;

public partial class TblPackage
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblTopup> TblTopups { get; set; } = new List<TblTopup>();
}
