# Personal Finance Management Microservices

This solution is a backend-only microservices architecture built with .NET 8, following Clean Architecture principles. It helps users track expenses, manage budgets, and understand their financial health via insights.

## Project Structure

Each microservice is organized into four projects:
- **API**: Controllers, Middleware, and Dependency Injection.
- **Application**: DTOs, Services, and Business logic.
- **Domain**: Entities and Interfaces.
- **Infrastructure**: Database implementations and Repositories.

## Microservices

1. **Auth Service**:
   - Manages user identities and authentication.
   - Database: **MongoDB**.
   - Endpoints: Register, Login, Me.

2. **Finance Service**:
   - Manages transactions (Income/Expense) and budgets.
   - Database: **SQL Server (EF Core)**.
   - Endpoints: Add/Get Transactions, Upsert/Get Budgets.

3. **Insights Service**:
   - Integration service that connects Auth and Finance.
   - Generates financial health scores and category trends.
   - Database: **SQL Server (EF Core)**.
   - Endpoint: Get Financial Health.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (Running on `localhost:27017`)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB supported)

## Setup Instructions

1. **Clone the repository.**
2. **Update Connection Strings**:
   - Check `appsettings.json` in each `.API` project.
   - `AuthService.API`: MongoDB connection.
   - `FinanceService.API`: SQL Server connection.
   - `InsightsService.API`: SQL Server connection.
3. **Database Migrations**:
   Run the following commands to create the SQL databases:
   ```bash
   # Finance Service
   cd FinanceService/FinanceService.API
   dotnet ef migrations add InitialCreate --project ../FinanceService.Infrastructure
   dotnet ef database update

   # Insights Service
   cd ../../InsightsService/InsightsService.API
   dotnet ef migrations add InitialCreate --project ../InsightsService.Infrastructure
   dotnet ef database update
   ```
4. **Run the Services**:
   Open three terminals and run each service:
   ```bash
   dotnet run --project AuthService/AuthService.API
   dotnet run --project FinanceService/FinanceService.API
   dotnet run --project InsightsService/InsightsService.API
   ```
5. **Swagger Documentation**:
   Once running, access Swagger at:
   - Auth Service: `https://localhost:7177/swagger`
   - Finance Service: `https://localhost:7104/swagger`
   - Insights Service: `https://localhost:7285/swagger`

## JWT Authentication

All requests to Finance and Insights services require a `Bearer` token received from the `AuthService.API/api/auth/login` endpoint.

---
Created by Antigravity AI.
