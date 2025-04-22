# ASP.NET Core Identity + JWT Auth Template

A starter template for ASP.NET Core Web API with:
- Identity + Role Management
- JWT Bearer Authentication
- Email Verification via Gmail (MailKit)
- Role-based API protection

## Project Structure

```bash
Project/
.
├── Config
│   └── AppConfig.cs
├── Controllers
│   ├── AccountController.cs
│   └── RolesController.cs
├── DTOs
│   ├── AssignRoleDto.cs
│   └── AuthDto.cs
├── Data
│   ├── DbSeeder.cs
│   └── DefaultDbContext.cs
├── Models
│   └── User.cs
├── Program.cs
├── README.md
├── Services
│   ├── AccountService
│   │   ├── AccountService.cs
│   │   └── IAccountService.cs
│   ├── EmailService
│   │   ├── EmailService.cs
│   │   └── IEmailService.cs
│   └── RolesService
│       ├── IRolesService.cs
│       └── RolesService.cs
└── appsettings.json
```

## 🔧 Tech Stack
- ASP.NET Core 8+
- Identity + Entity Framework
- MailKit
- JWT Bearer Auth

## 🚀 Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/gibatekpro/Net-Core-Identity-User-Template.git
cd Net-Core-Identity-User-Template
```

### 2. Update `appsettings.json`
```json
{
    "AppConfig": {
        "Jwt": {
            "Key": "your_jwt_secret",
            "Issuer": "https://localhost:44300",
            "ExpireHours": 1
        },
        "EmailSettings": {
            "SmtpServer": "smtp.gmail.com",
            "SmtpPort": 587,
            "SmtpUsername": "youraccount@gmail.com",
            "SmtpPassword": "your-app-password"
        }
    },
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=IdentityDb;Trusted_Connection=True;"
    }
}
```

### 4. Add Packages
Entity Framework CLI tool - The command-line interface (CLI) tools for Entity Framework Core. [Documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
```bash
dotnet tool install --global dotnet-ef
```

***
Entity Framework - Data access using a model [Documentation]( )

```bash
dotnet add package Microsoft.EntityFrameworkCore
```

***
Entity Framework Tools - Tools for Entity Framework [Documentation](https://learn.microsoft.com/en-us/ef/core/)

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

***
Entity Framework SQLite - Allows Entity Framework Core to be used with SQLite. [Documentation](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/?tabs=dotnet-core-cli)
```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

***
Entity Framework SQL Server - Allows Entity Framework Core to be used with Microsoft SQL Server. [Documentation](https://learn.microsoft.com/en-us/ef/core/)
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

***
Identity - Provides types for persisting Identity data with Entity Framework Core. [Documentation](https://learn.microsoft.com/en-us/ef/core/)
```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

***
ASP.NET Scaffolding - Generates boilerplate code for web apps to speed up development. [Documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

```bash
dotnet tool install -g dotnet-aspnet-codegenerator
```

```bash
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 9.0.0
```

***
Entity Framework Design - The Entity Framework Core tools help with design-time development tasks. [Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.device.location.geocoordinate?view=netframework-4.8.1)
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
```

***
Auto Mapper - Map models and entities. [Documentation](https://docs.automapper.org/en/stable/)
```bash
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

***
MailKit For Email Verification using SMTP [Documentation](https://dotnetfoundation.org/news-events/detail/mailkit-working-with-emails)
```bash
dotnet add package MailKit
```

***
JwT
```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```
***
Scrutor: For tracking dependencies without having to write them in Program.cs [Documentation](https://github.com/khellang/Scrutor)
```bash
dotnet add package Scrutor
```

### 4. Setup Database
```bash
dotnet ef migrations add Initial
```
```bash
dotnet ef database update
```

### 5. Run the Project
```bash
dotnet run
```

## 🧪 API Endpoints (Test with Postman)
- `POST /api/account/register`
- `GET /api/account/verify-email`
- `POST /api/account/login`
- `POST /api/roles` (Admin only)
- `POST /api/roles/assign` (Admin only)

## ✅ Notes
- Requires .NET 8 SDK and above
- Enable 2FA and generate Gmail App Password for email to work
- JWT is valid for `ExpireHours`
- Create roles manually or in a seed script
