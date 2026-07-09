# YBS Smart Card System

YBS Smart Card System is a .NET 8 solution for managing bus smart cards, top-up packages, card balances, bus fare taps, and card transaction history.

## Project Structure

- `YbsSmartCardSystem.Api` - ASP.NET Core Web API with Swagger.
- `YbsSmartCardSystem.App` - Blazor Server UI that calls the API.
- `YbsSmartCardSystem.Domain` - Feature services, request models, and response models.
- `YbsSmartCardSystem.Database` - EF Core database context and table models.

## Main Workflow

1. Create a card from the Cards page or `POST /api/Card`.
2. Create top-up packages from the Packages page or `POST /api/Package`.
3. Top up a card by selecting a card number and package.
4. Tap the card on a bus to deduct the configured fare amount.
5. Review the card transaction history from the Transactions page.

## Features

- Card search, registration, and detail view.
- Package list, detail, create, update, and soft delete.
- Card top-up using configured packages.
- Bus tap payment using `BusFareAmount`.
- Transaction list and detail view.
- Paginated listing pages and listing API endpoints.

## API Endpoints

- `GET /api/Card`
- `GET /api/Card/{cardId}`
- `POST /api/Card`
- `GET /api/Package`
- `GET /api/Package/{packageId}`
- `POST /api/Package`
- `PUT /api/Package/{packageId}`
- `DELETE /api/Package/{packageId}`
- `POST /api/Topup`
- `POST /api/BusPayment/tap`
- `GET /api/Transaction`
- `GET /api/Transaction/{transactionId}`

Listing endpoints use `PageNo` and `PageSize` query parameters and return pagination metadata.

## Run Locally

Update the database connection string in `YbsSmartCardSystem.Api/appsettings.json`, then run:

```bash
dotnet build
dotnet run --project YbsSmartCardSystem.Api --launch-profile https
dotnet run --project YbsSmartCardSystem.App --launch-profile https
```

Default local URLs:

- API Swagger: `https://localhost:7248/swagger`
- Blazor App: `https://localhost:7177`

## Database Scaffolding

The database project is DB-first. To refresh EF Core models:

```bash
dotnet ef dbcontext scaffold "<connection-string>" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext --no-onconfiguring -f
```

Run the command from the `YbsSmartCardSystem.Database` project directory.

## Development Rules

See `AGENTS.md` for API contract rules. In short: each endpoint owns its request and response models, listing endpoints must be paginated, and API responses must not return EF Core entities directly.
