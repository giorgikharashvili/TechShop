# TechShop Web API

TechShop Web API is a backend service built with ASP.NET Core Web API. It provides a RESTful interface for managing tech products such as computers, mice, keyboards, and accessories. This API is the core backend for the TechShop platform.

---

## What the Project Does

- Manage tech product inventory (CRUD operations)
- Handle user accounts (registration, login)
- Process and track customer orders
- Supports integration with a frontend

---

## Why the Project is Useful

- Demonstrates a clean architecture using ASP.NET Core Web API
- Provides a scalable and modular backend for any e-commerce frontend
- Great for learning RESTful API development and best practices in .NET

---

## Getting Started

### Prerequisites

- [.NET SDK 9.0](https://dotnet.microsoft.com/download)
- SQL Server (or a local SQL Server Express instance)
- Git

### Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/giorgikharashvili/TechShop.git
   cd TechShop
   
2. **Configure the database connection**
   Open appsettings.json and set your SQL Server connection
   
3. **Apply migrations and update the database**
   ```Package Manager Console
   add-migration Init
   update-database
4. **Run the API**


📚 API Documentation

Use Swagger to explore and test the API:
Navigate to https://localhost:5001/swagger in your browser once the app is running.

❓ Where to Get Help

Open an issue on in the tab of this repository

👥 Maintainers and Contributors

Maintained by: @giorgikharashvili

Mentor: @viachaslaukitsun – code review and guidance






   
   
   

