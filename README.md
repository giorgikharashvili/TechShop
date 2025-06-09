# 🛒 TechShop Web API

**TechShop** is a modular, scalable **e-commerce backend** built with **ASP.NET Core Web API**. It provides a RESTful interface to manage products, users, carts, orders, addresses, and payment integrations. Designed following clean architecture principles using Dapper and MediatR (CQRS), it's ideal for learning and production-ready deployment.

---

## 🚀 Features

- 🧾 **Product Management** — CRUD operations on tech items like computers, accessories, and components
- 👤 **User System** — Registration, JWT authentication, and role-based access (`Admin`, `Manager`, `Customer`)
- 🛒 **Cart & Wishlist** — Add, update, and manage cart and wishlist items
- 📦 **Order Handling** — Create and track customer orders
- 💳 **Stripe Payments** — Integrated with Stripe for secure transaction processing
- 🧰 **CQRS with MediatR** — Clear separation of command/query responsibility
- ⚙️ **Dapper Repositories** — Lightweight and performant SQL data access
- 📊 **Rate Limiting & Role-Based Authorization**

---

## 🛠 Getting Started

### ✅ Prerequisites

- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)
- A free [Stripe](https://stripe.com) account

---

### 📦 Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/giorgikharashvili/TechShop.git
   cd TechShop
   ```

2. **Configure environment variables**

   Set the required environment variables for your database and Stripe API key:

   **Windows (PowerShell):**
   ```powershell
   $env:DB_CONNECTION="Server=localhost;Database=YourDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
   $env:STRIPE_SECRET_KEY="your_stripe_secret_key"
   ```

   **Linux/macOS (Bash):**
   ```bash
   export DB_CONNECTION="Server=localhost;Database=YourDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
   export STRIPE_SECRET_KEY="your_stripe_secret_key"
   ```

3. **Apply the initial database script**

   Navigate to the `Scripts/` directory and execute the provided SQL file in SQL Server Management Studio or via command line to initialize the schema and seed basic data.

4. **Run the Web API**
   ```bash
   dotnet run --project TechShop.WebAPI
   ```

5. **Access Swagger documentation**

   Navigate to:

   [https://localhost:7182/swagger](https://localhost:7182/swagger)

---

## 📂 Project Structure

```
TechShop/
│
├── TechShop.WebAPI          → API Layer (Controllers, Middleware, Auth)
├── TechShop.Application     → CQRS (Commands, Queries, DTOs)
├── TechShop.Domain          → Core Entities and Interfaces
├── TechShop.Infrastructure  → Dapper Repositories, DB Access, Payment Services
├── Scripts/                 → SQL scripts for initial DB setup
```

---

## 🔐 Authentication & Roles

TechShop uses **JWT-based authentication** with role support:
- `Admin` – Full access
- `Manager` – Moderate access
- `Customer` – User-facing features only

Secure endpoints are protected with `[Authorize(Roles = "...")]`, and request throttling is enforced with ASP.NET Rate Limiting policies.

---

## 💳 Stripe Integration

To enable Stripe payments in your application, follow these steps:

### 1. Create a Stripe Account
- Sign up or log in at [https://stripe.com](https://stripe.com)

### 2. Obtain API Keys
- Go to your Stripe Dashboard → Developers → API keys  
- Copy your **Secret Key** and set it as an environment variable:

```bash
export STRIPE_SECRET_KEY=your_secret_key_here
```

---

### 3. Local Testing with Stripe CLI

#### 🔽 Download the Stripe CLI
- Download from [Stripe CLI GitHub Releases](https://github.com/stripe/stripe-cli/releases/tag/v1.27.0)
- Choose the version appropriate for your system (Windows, macOS, Linux)
- Extract the files into a folder named `stripe-cli`

#### 🖥️ Run Stripe CLI
Open your terminal and navigate to the extracted folder:

```bash
cd path/to/stripe-cli
```

#### 🔐 Log in to Stripe
Run the login command and authenticate through your browser:

```bash
stripe login
```

---

### 4. Webhook Forwarding

To test Stripe webhooks locally, forward events to your local server:

```bash
stripe listen --forward-to https://localhost:7182/api/webhooks/stripe
```

This will listen for Stripe events and forward them to your backend.

---

### 5. Trigger a Webhook for Testing

You can simulate a successful payment event with:

```bash
stripe trigger payment_intent.succeeded
```

---

### ✅ You're all set!

Stripe is now integrated and ready for local development and testing.  

---

## 🧪 Example Endpoints

| Method | Endpoint                     | Description                    |
|--------|------------------------------|--------------------------------|
| `POST` | `/api/Auth/login`            | Authenticate and return JWT    |
| `GET`  | `/api/Products`              | Retrieve product catalog       |
| `POST` | `/api/Cart/Add`              | Add item to user cart          |
| `PUT`  | `/api/Addresses/{id}`        | Update address by ID           |

---

## 👥 Maintainers

- 🧑‍💻 Author: [@giorgikharashvili](https://github.com/giorgikharashvili)
- 🧠 Mentor: [@viachaslaukitsun](https://github.com/viachaslaukitsun)

---
