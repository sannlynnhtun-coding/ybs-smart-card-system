using System;
using System.Collections.Generic;

namespace YbsSmartCardSystem.Database.AppDbContextModels;

public partial class TblTopup
{
    public int TopupId { get; set; }

    public int CardId { get; set; }

    public int PackageId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblCard Card { get; set; } = null!;

    public virtual TblPackage Package { get; set; } = null!;
}
