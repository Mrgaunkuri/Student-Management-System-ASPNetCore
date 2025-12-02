# Student Management System

A simple student management web app built with ASP.NET Core (.NET 9). It provides user authentication/authorization (Identity), role-based access, and full CRUD for students via Razor Pages / MVC views.

## Key features
- ASP.NET Core Identity with roles (Admin, Teacher, Student) and seeded admin account.
- CRUD operations for student records with role-restricted actions.
- Razor views following framework conventions and extensible controllers.
- SQL Server (configured via connection string) and EF Core migrations support.

## Prerequisites
- .NET 9 SDK installed — download from https://dotnet.microsoft.com.
- SQL Server instance (local or remote) and a connection string.
- Optional: Visual Studio 2022/2023 or VS Code for development.

## Quick start (local)
1. Clone the repo:
   - git clone https:[//github.com/Mrgaunkuri/A-simple-Student-Management-System-built-using-ASP.NET-Core-MVC-with-full-CRUD-operations.](https://github.com/Mrgaunkuri/Student-Management-System-ASPNetCore.git)
2. Configure database:
   - Copy `appsettings.example.json` → `appsettings.Development.json` and set `DefaultConnection`.
3. Apply database migrations (if present) or enable automatic migrations:
   - dotnet tool install --global dotnet-ef (if needed)  
   - dotnet ef database update
4. Run the app:
   - From terminal: `dotnet run --project "Student management system.csproj"`
   - Or open the solution in Visual Studio and use __Build__ then __Run__.
5. First-time seed:
   - The app runs a seeder (DbInitializer) at startup that adds roles and an admin user; ensure the DB is reachable.

## Configuration
- Connection string and seed credentials: configure in `appsettings.Development.json` (see `appsettings.example.json`).
- Seed override keys:
  - `Seed:AdminEmail`
  - `Seed:AdminPassword`

## Usage / Examples
- Default landing page: `/` (Home/Index).
- Login: `/Account/Login` — use seeded admin or register a new user.
- Students list: `/Student/Index` (requires appropriate role).
- To change port when running via `dotnet run`, set `ASPNETCORE_URLS=http://localhost:5002`.

## Project layout (important files)
- `Program.cs` — app startup, DI configuration, routing, and seeding.
- `model/` — EF models, `ApplicationDBContext`, `DbInitializer`.
- `Controller/` and `Views/` — MVC controllers and views (Razor).
- `Views/Shared/_Layout.cshtml` — site layout and nav.
- `appsettings.example.json` — example configuration (do not store secrets).

## Contributing
- See `CONTRIBUTING.md` for branch, PR, and commit conventions.
- Before opening PRs: run formatters and tests, keep changes scoped, and reference related issues.

## License & Code of Conduct
- Add a `LICENSE` file before publishing. Choose MIT/Apache/GPL according to reuse goals.
- Consider adding `CODE_OF_CONDUCT.md` for community guidelines.

## Releases / changelog
- Use `CHANGELOG.md` or GitHub Releases for notable changes and version tags.

## Troubleshooting
- Scoped DI error when seeding roles? Ensure the seeder is called with the scoped provider:
  - `using var scope = app.Services.CreateScope(); await DbInitializer.SeedRolesAndAdminAsync(scope.ServiceProvider);`
- If views fail to render after converting between Razor Pages and MVC, verify `Views/_ViewImports.cshtml` namespace and view locations (`Views/Home/Index.cshtml`).
