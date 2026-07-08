using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YbsSmartCardSystem.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCard> TblCards { get; set; }

    public virtual DbSet<TblPackage> TblPackages { get; set; }

    public virtual DbSet<TblTopup> TblTopups { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__Tbl_Card__55FECDAE13F03D72");

            entity.ToTable("Tbl_Card");

            entity.HasIndex(e => e.CardNo, "UQ_Tbl_Card_CardNo").IsUnique();

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CardNo).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
        });

        modelBuilder.Entity<TblPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Tbl_Pack__322035CC61CB83B3");

            entity.ToTable("Tbl_Package");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.PackageName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblTopup>(entity =>
        {
            entity.HasKey(e => e.TopupId).HasName("PK__Tbl_Topu__81D777BB96898B26");

            entity.ToTable("Tbl_Topup");

            entity.HasIndex(e => e.CardId, "IX_Tbl_Topup_CardId");

            entity.HasIndex(e => e.PackageId, "IX_Tbl_Topup_PackageId");

            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);

            entity.HasOne(d => d.Card).WithMany(p => p.TblTopups)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Topup_Card");

            entity.HasOne(d => d.Package).WithMany(p => p.TblTopups)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Topup_Package");
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Tbl_Tran__55433A6B9D410F25");

            entity.ToTable("Tbl_Transaction");

            entity.HasIndex(e => e.CardId, "IX_Tbl_Transaction_CardId");

            entity.HasIndex(e => e.TransactionNo, "UQ_Tbl_Transaction_TransactionNo").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.TransactionNo).HasMaxLength(50);

            entity.HasOne(d => d.Card).WithMany(p => p.TblTransactions)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Card");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
