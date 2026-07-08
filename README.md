```
dotnet ef dbcontext scaffold "Server=.;Database=YBSSmartCardSystem;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext --no-onconfiguring -f
```