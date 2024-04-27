CREATE DATABASE StationeryCompany;
USE StationeryCompany;

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL,
    Type VARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    SupplierName VARCHAR(255) NOT NULL,
    SupplierContact VARCHAR(100) NOT NULL
);

CREATE TABLE Sales (
    SaleID INT PRIMARY KEY IDENTITY,
    Manager VARCHAR(100) NOT NULL,
    CustomerName VARCHAR(255) NOT NULL,
    SalesManager VARCHAR(100) NOT NULL,
    QuantitySold INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    SaleDate DATE NOT NULL,
    ProductID INT NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE SalesManagers (
    ManagerID INT PRIMARY KEY IDENTITY,
    ManagerName VARCHAR(100) NOT NULL
);

CREATE TABLE CustomerCompanies (
    CompanyID INT PRIMARY KEY IDENTITY,
    CompanyName VARCHAR(255) NOT NULL
);

INSERT INTO Products (Name, Type, Quantity, CostPrice, SupplierName, SupplierContact)
VALUES ('Pen', 'Stationery', 100, 1.50, 'ABC Suppliers', 'abc@example.com'),
       ('Notebook', 'Stationery', 50, 2.00, 'XYZ Suppliers', 'xyz@example.com');

INSERT INTO Sales (Manager, CustomerName, SalesManager, QuantitySold, UnitPrice, SaleDate, ProductID)
VALUES ('John Doe', 'Customer A', 'Sales Manager A', 10, 1.50, '2024-04-27', 1),
       ('Jane Smith', 'Customer B', 'Sales Manager B', 5, 2.00, '2024-04-28', 2);

CREATE PROCEDURE InsertSalesManager
    @ManagerName VARCHAR(100)
AS
BEGIN
    INSERT INTO SalesManagers (ManagerName)
    VALUES (@ManagerName);
END;
GO

CREATE PROCEDURE InsertCustomerCompany
    @CompanyName VARCHAR(255)
AS
BEGIN
    INSERT INTO CustomerCompanies (CompanyName)
    VALUES (@CompanyName);
END;
GO

CREATE PROCEDURE UpdateProduct
    @ProductID INT,
    @Name VARCHAR(255),
    @Type VARCHAR(50),
    @Quantity INT,
    @CostPrice DECIMAL(10, 2),
    @SupplierName VARCHAR(255),
    @SupplierContact VARCHAR(100)
AS
BEGIN
    UPDATE Products
    SET Name = @Name,
        Type = @Type,
        Quantity = @Quantity,
        CostPrice = @CostPrice,
        SupplierName = @SupplierName,
        SupplierContact = @SupplierContact
    WHERE ProductID = @ProductID;
END;

CREATE PROCEDURE UpdateCustomerCompany
    @CompanyID INT,
    @CompanyName VARCHAR(255)
AS
BEGIN
    UPDATE CustomerCompanies
    SET CompanyName = @CompanyName
    WHERE CompanyID = @CompanyID;
END;

CREATE PROCEDURE UpdateSalesManager
    @ManagerID INT,
    @ManagerName VARCHAR(100)
AS
BEGIN
    UPDATE SalesManagers
    SET ManagerName = @ManagerName
    WHERE ManagerID = @ManagerID;
END;

CREATE PROCEDURE UpdateProductType
    @TypeID INT,
    @TypeName VARCHAR(50)
AS
BEGIN
    UPDATE ProductTypes
    SET TypeName = @TypeName
    WHERE TypeID = @TypeID;
END;

CREATE PROCEDURE DeleteProduct
    @ProductID INT
AS
BEGIN
    INSERT INTO Products_Archive (ProductID, Name, Type, Quantity, CostPrice, SupplierName, SupplierContact)
    SELECT ProductID, Name, Type, Quantity, CostPrice, SupplierName, SupplierContact
    FROM Products
    WHERE ProductID = @ProductID;

    DELETE FROM Products
    WHERE ProductID = @ProductID;
END;
GO

CREATE PROCEDURE DeleteSalesManager
    @ManagerID INT
AS
BEGIN
    INSERT INTO SalesManagers_Archive (ManagerID, ManagerName)
    SELECT ManagerID, ManagerName
    FROM SalesManagers
    WHERE ManagerID = @ManagerID;

    DELETE FROM SalesManagers
    WHERE ManagerID = @ManagerID;
END;
GO

CREATE PROCEDURE DeleteProductType
    @TypeID INT
AS
BEGIN
    INSERT INTO ProductTypes_Archive (TypeID, TypeName)
    SELECT TypeID, TypeName
    FROM ProductTypes
    WHERE TypeID = @TypeID;

    DELETE FROM ProductTypes
    WHERE TypeID = @TypeID;
END;
GO

CREATE PROCEDURE DeleteCustomerCompany
    @CompanyID INT
AS
BEGIN
    INSERT INTO CustomerCompanies_Archive (CompanyID, CompanyName)
    SELECT CompanyID, CompanyName
    FROM CustomerCompanies
    WHERE CompanyID = @CompanyID;

    DELETE FROM CustomerCompanies
    WHERE CompanyID = @CompanyID;
END;
GO

CREATE TABLE ProductTypes (
    TypeID INT PRIMARY KEY IDENTITY,
    TypeName VARCHAR(50) NOT NULL
);

INSERT INTO ProductTypes (TypeName)
VALUES ('Stationery');

SELECT * FROM Products WHERE Type = 'Тип, який ви хочете видалити';

DELETE FROM Products WHERE Type = 'Тип, який ви хочете видалити';

DELETE FROM ProductTypes WHERE TypeID = Ваш_ID_типу_канцтовару;


