using System;
using System.Collections.Generic;

namespace YbsSmartCardSystem.Database.AppDbContextModels;

public partial class TblCard
{
    public int CardId { get; set; }

    public string CardNo { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblTopup> TblTopups { get; set; } = new List<TblTopup>();

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
