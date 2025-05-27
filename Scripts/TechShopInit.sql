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


-- auth.Users table
CREATE INDEX IdxUsersCreatedAt ON auth.Users(CreatedAt);
CREATE INDEX IdxUsersModifiedAt ON auth.Users(ModifiedAt);
CREATE INDEX IdxUsersCreatedBy ON auth.Users(CreatedBy);
CREATE INDEX IdxUsersModifiedBy ON auth.Users(ModifiedBy);

-- auth.Addresses table
CREATE INDEX IdxAddressesCity ON auth.Addresses(City);
CREATE INDEX IdxAddressesPostalCode ON auth.Addresses(PostalCode);

-- catalog.Categories table
CREATE INDEX IdxCategoriesName ON catalog.Categories(Name);

-- catalog.Products table
CREATE INDEX IdxProductsCategoryId ON catalog.Products(CategoryId);
CREATE INDEX IdxProductsCreatedAt ON catalog.Products(CreatedAt);
CREATE INDEX IdxProductsCreatedBy ON catalog.Products(CreatedBy);
CREATE INDEX IdxProductsModifiedAt ON catalog.Products(ModifiedAt);
CREATE INDEX IdxProductsModifiedBy ON catalog.Products(ModifiedBy);

-- catalog.ProductsSkus table
CREATE INDEX IdxProductsSkusProductId ON catalog.ProductsSkus(ProductId);
CREATE INDEX IdxProductsSkusSku ON catalog.ProductsSkus(Sku);

-- catalog.ProductSkuAttributes table
CREATE INDEX IdxProductSkuAttributesType ON catalog.ProductSkuAttributes(Type);

-- cart.Wishlist table
CREATE INDEX IdxWishlistProductId ON cart.Wishlist(ProductId);
CREATE INDEX IdxWishlistUserId ON cart.Wishlist(UserId);
CREATE INDEX IdxWishlistCreatedAt ON cart.Wishlist(CreatedAt);
CREATE INDEX IdxWishlistCreatedBy ON cart.Wishlist(CreatedBy);
CREATE INDEX IdxWishlistModifiedAt ON cart.Wishlist(ModifiedAt);
CREATE INDEX IdxWishlistModifiedBy ON cart.Wishlist(ModifiedBy);

-- cart.CartItem table
CREATE INDEX IdxCartItemCartId ON cart.CartItem(CartId);
CREATE INDEX IdxCartItemProductId ON cart.CartItem(ProductId);
CREATE INDEX IdxCartItemProductSkuId ON cart.CartItem(ProductSkuId);

-- orders.OrderDetails table
CREATE INDEX IdxOrderDetailsCreatedAt ON orders.OrderDetails(CreatedAt);
CREATE INDEX IdxOrderDetailsCreatedBy ON orders.OrderDetails(CreatedBy);
CREATE INDEX IdxOrderDetailsModifiedAt ON orders.OrderDetails(ModifiedAt);
CREATE INDEX IdxOrderDetailsModifiedBy ON orders.OrderDetails(ModifiedBy);

-- orders.OrderItem table
CREATE INDEX IdxOrderItemOrderId ON orders.OrderItem(OrderId);
CREATE INDEX IdxOrderItemProductId ON orders.OrderItem(ProductId);
CREATE INDEX IdxOrderItemProductsSkuId ON orders.OrderItem(ProductsSkuId);

-- orders.Payments table
CREATE INDEX IdxPaymentsOrderId ON orders.Payments(OrderId);
CREATE INDEX IdxPaymentsStatus ON orders.Payments(Status);
CREATE INDEX IdxPaymentsCreatedAt ON orders.Payments(CreatedAt);
CREATE INDEX IdxPaymentsCreatedBy ON orders.Payments(CreatedBy);
CREATE INDEX IdxPaymentsModifiedAt ON orders.Payments(ModifiedAt);
CREATE INDEX IdxPaymentsModifiedBy ON orders.Payments(ModifiedBy);