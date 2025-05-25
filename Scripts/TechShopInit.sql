-- Drop tables with schema prefix
DROP TABLE IF EXISTS orders.Payments;
DROP TABLE IF EXISTS orders.OrderItem;
DROP TABLE IF EXISTS orders.OrderDetails;
DROP TABLE IF EXISTS cart.CartItem;
DROP TABLE IF EXISTS cart.Cart;
DROP TABLE IF EXISTS cart.Wishlist;
DROP TABLE IF EXISTS catalog.ProductSkuAttributes;
DROP TABLE IF EXISTS catalog.ProductsSkus;
DROP TABLE IF EXISTS catalog.Products;
DROP TABLE IF EXISTS catalog.Categories;
DROP TABLE IF EXISTS auth.Addresses;
DROP TABLE IF EXISTS auth.Users;

-- == AUTH SCHEMA == --

CREATE TABLE auth.Users (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  FirstName NVARCHAR(255),
  LastName NVARCHAR(255),
  Username NVARCHAR(255) UNIQUE,
  Email NVARCHAR(255) UNIQUE,
  PasswordHash NVARCHAR(255),
  PhoneNumber NVARCHAR(255),
  CreatedAt DATETIME,
  CreatedBy NVARCHAR(255),
  ModifiedAt DATETIME,
  ModifiedBy NVARCHAR(255)
);

CREATE INDEX IdxCreatedAt ON auth.Users(CreatedAt);

CREATE TABLE auth.Addresses (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  UserId INT FOREIGN KEY REFERENCES auth.Users(Id),
  AddressLine1 NVARCHAR(255),
  AddressLine2 NVARCHAR(255),
  Country NVARCHAR(255),
  City NVARCHAR(255),
  PostalCode NVARCHAR(255),
  CreatedAt DATETIME,
  CreatedBy NVARCHAR(255),
  ModifiedAt DATETIME,
  ModifiedBy NVARCHAR(255)
);

-- == CATALOG SCHEMA == --

CREATE TABLE catalog.Categories (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  Name NVARCHAR(255),
  Description NVARCHAR(255)
);

CREATE TABLE catalog.Products (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  Name NVARCHAR(255),
  Description NVARCHAR(255),
  CategoryId INT FOREIGN KEY REFERENCES catalog.Categories(Id),
  CreatedAt DATETIME,
  CreatedBy NVARCHAR(255),
  ModifiedAt DATETIME,
  ModifiedBy NVARCHAR(255)
);

CREATE TABLE catalog.ProductsSkus (
  Id INT PRIMARY KEY,
  ProductId INT FOREIGN KEY REFERENCES catalog.Products(Id),
  Price DECIMAL,
  Sku NVARCHAR(255),
  StockQuantity NVARCHAR(255)
);

CREATE TABLE catalog.ProductSkuAttributes (
  Id INT PRIMARY KEY,
  Type NVARCHAR(255),
  Value NVARCHAR(255),
  FOREIGN KEY (Id) REFERENCES catalog.ProductsSkus(Id)
);

-- == CART SCHEMA == --

CREATE TABLE cart.Wishlist (
  Id INT PRIMARY KEY,
  ProductId INT FOREIGN KEY REFERENCES catalog.Products(Id),
  UserId INT FOREIGN KEY REFERENCES auth.Users(Id),
  CreatedAt DATETIME,
  CreatedBy NVARCHAR(255),
  ModifiedAt DATETIME,
  ModifiedBy NVARCHAR(255)
);

CREATE TABLE cart.Cart (
  Id INT PRIMARY KEY,
  UserId INT UNIQUE FOREIGN KEY REFERENCES auth.Users(Id),
  TotalPrice INT
);

CREATE TABLE cart.CartItem (
  Id INT PRIMARY KEY,
  CartId INT FOREIGN KEY REFERENCES cart.Cart(Id),
  ProductId INT FOREIGN KEY REFERENCES catalog.Products(Id),
  ProductSkuId INT FOREIGN KEY REFERENCES catalog.ProductsSkus(Id),
  Quantity INT
);

-- == ORDERS SCHEMA == -- 

CREATE TABLE orders.OrderDetails (
  Id INT PRIMARY KEY,
  UserId INT FOREIGN KEY REFERENCES auth.Users(Id),
  TotalPrice INT,
  CreatedAt DATETIME,
  CreatedBy NVARCHAR(255),
  ModifiedAt DATETIME,
  ModifiedBy NVARCHAR(255)
);

CREATE TABLE orders.OrderItem (
  Id INT PRIMARY KEY,
  OrderId INT FOREIGN KEY REFERENCES orders.OrderDetails(Id),
  ProductId INT FOREIGN KEY REFERENCES catalog.Products(Id),
  ProductsSkuId INT FOREIGN KEY REFERENCES catalog.ProductsSkus(Id),
  Quantity INT
);

CREATE TABLE orders.Payments (
  Id INT PRIMARY KEY IDENTITY(1, 1),
  OrderId INT FOREIGN KEY REFERENCES orders.OrderDetails(Id),
  StripePaymentId NVARCHAR(255),
  Amount DECIMAL,
  Currency NVARCHAR(255),
  Status NVARCHAR(255),
  CreatedAt DATETIME,
  CreatedBy NVARCHAR(255),
  ModifiedAt DATETIME,
  ModifiedBy NVARCHAR(255)
);
