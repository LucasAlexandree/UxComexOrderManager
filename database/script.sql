
/*
  UXComex - Simplified "Order Management" (SQL Server)
  Run this script on your SQL Server instance prior to starting the app.
  Adjust the connection string in appsettings.json accordingly.
*/

IF DB_ID('UxComexOrdersDb') IS NULL
BEGIN
  CREATE DATABASE UxComexOrdersDb;
END
GO

USE UxComexOrdersDb;
GO

-- Drop tables if re-running (dev convenience)
IF OBJECT_ID('dbo.OrderItems', 'U') IS NOT NULL DROP TABLE dbo.OrderItems;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL DROP TABLE dbo.Products;
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE dbo.Customers;
IF OBJECT_ID('dbo.Notifications', 'U') IS NOT NULL DROP TABLE dbo.Notifications;

CREATE TABLE dbo.Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Phone NVARCHAR(50) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Description NVARCHAR(400) NULL,
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0
);

CREATE TABLE dbo.Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    OrderDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
    Status NVARCHAR(20) NOT NULL DEFAULT 'New',
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(Id)
);

CREATE TABLE dbo.OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES dbo.Orders(Id),
    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(Id)
);

-- Optional "notifications" table for status changes (desirable requirement)
CREATE TABLE dbo.Notifications (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    OldStatus NVARCHAR(20) NULL,
    NewStatus NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Message NVARCHAR(400) NOT NULL,
    CONSTRAINT FK_Notifications_Orders FOREIGN KEY (OrderId) REFERENCES dbo.Orders(Id)
);

-- Seed initial data
INSERT INTO dbo.Customers (Name, Email, Phone)
VALUES 
('Alice Santos','alice@example.com','11999990000'),
('Bruno Lima','bruno@example.com','11911112222'),
('Carla Souza','carla@example.com','11933334444');

INSERT INTO dbo.Products (Name, Description, Price, StockQuantity)
VALUES
('Notebook 14"', 'Windows laptop 14 inches', 3500.00, 15),
('Mouse Gamer', 'Ergonomic mouse', 120.00, 100),
('Teclado Mecânico', 'Blue switches', 280.00, 40),
('Monitor 24"', '1080p IPS', 799.90, 20);
