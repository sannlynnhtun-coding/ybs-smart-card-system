using Microsoft.EntityFrameworkCore;
using YbsSmartCardSystem.Database.AppDbContextModels;
using YbsSmartCardSystem.Domain.Features.BusPayment;
using YbsSmartCardSystem.Domain.Features.Card;
using YbsSmartCardSystem.Domain.Features.Package;
using YbsSmartCardSystem.Domain.Features.Topup;
using YbsSmartCardSystem.Domain.Features.Transaction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt=>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<TopupService>();
builder.Services.AddScoped<TransactionService>();

decimal busFareAmount = decimal.TryParse(builder.Configuration["BusFareAmount"], out var fareAmount)
    ? fareAmount
    : 500;

builder.Services.AddScoped(sp =>
    new BusPaymentService(sp.GetRequiredService<AppDbContext>(), busFareAmount));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
