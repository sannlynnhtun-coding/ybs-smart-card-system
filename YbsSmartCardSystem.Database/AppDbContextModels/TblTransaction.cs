using System;
using System.Collections.Generic;

namespace YbsSmartCardSystem.Database.AppDbContextModels;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public string TransactionNo { get; set; } = null!;

    public int CardId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblCard Card { get; set; } = null!;
}
