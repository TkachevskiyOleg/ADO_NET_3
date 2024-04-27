using System;
using System.Data.SqlClient;

class Program
{
    static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StationeryCompany;Integrated Security=True;Connect Timeout=30;";

    static void Main()
    {
        if (TestConnection())
        {
            Console.WriteLine("Connection to the database successful");

            

            int choice;
            bool isValidChoice;

            do
            {
                Console.Write("Enter your choice: ");
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Display all products");
                Console.WriteLine("2. Display all product types");
                Console.WriteLine("3. Display all sales managers");
                Console.WriteLine("4. Display products with maximum quantity");
                Console.WriteLine("5. Display products with minimum quantity");
                Console.WriteLine("6. Display products with minimum cost price");
                Console.WriteLine("7. Display products with maximum cost price");
                Console.WriteLine("8. Display products by type");
                Console.WriteLine("9. Display products sold by manager");
                Console.WriteLine("10. Display products purchased by company");
                Console.WriteLine("11. Display recent sales information");
                Console.WriteLine("12. Display average quantity by product type");
                Console.WriteLine("13. Insert new product, product type, sales manager, or customer company");
                Console.WriteLine("14. Update existing product, customer company, sales manager, or product type");
                Console.WriteLine("15. Delete product, sales manager, product type, or customer company");
                Console.WriteLine("16. Exit");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);

                if (isValidChoice)
                {
                    switch (choice)
                    {
                        case 1:
                            DisplayAllProducts();
                            break;
                        case 2:
                            DisplayAllProductTypes();
                            break;
                        case 3:
                            DisplayAllSalesManagers();
                            break;
                        case 4:
                            DisplayProductsWithMaxQuantity();
                            break;
                        case 5:
                            DisplayProductsWithMinQuantity();
                            break;
                        case 6:
                            DisplayProductsWithMinCostPrice();
                            break;
                        case 7:
                            DisplayProductsWithMaxCostPrice();
                            break;
                        case 8:
                            DisplayProductsByType("Pens");
                            break;
                        case 9:
                            DisplayProductsSoldByManager("John Doe");
                            break;
                        case 10:
                            DisplayProductsPurchasedByCompany("ABC Company");
                            break;
                        case 11:
                            DisplayRecentSalesInfo();
                            break;
                        case 12:
                            DisplayAverageQuantityByProductType();
                            break;
                        case 13:
                            InsertNewProduct("New Pen", "Stationery", 50, 2, "New Supplier", "new@supplier.com");
                            InsertNewProductType("New Type");
                            InsertNewSalesManager("New Manager");
                            InsertNewCustomerCompany("New Company");
                            break;
                        case 14:
                            UpdateExistingProduct(1, "Updated Pen", "Updated Type", 75, 2, "Updated Supplier", "updated@supplier.com");
                            UpdateExistingCustomerCompany(1, "Updated Company");
                            UpdateExistingSalesManager(1, "Updated Manager");
                            UpdateExistingProductType(1, "Updated Type");
                            break;
                        case 15:
                            
                            DeleteProduct(1);
                            DeleteSalesManager(1);
                            DeleteProductType(1);
                            DeleteCustomerCompany(1);
                            break;
                        case 16:
                            
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                }
            } while (!isValidChoice);
        }
    }


    static bool TestConnection()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }

    static void DisplayAllProducts()
    {
        Console.WriteLine("\n--- All Products ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Products";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Quantity: {reader["Quantity"]}, CostPrice: {reader["CostPrice"]}");
            }
            reader.Close();
        }
    }

    static void DisplayAllProductTypes()
    {
        Console.WriteLine("\n--- All Product Types ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT Type FROM Products";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Type: {reader["Type"]}");
            }
            reader.Close();
        }
    }

    static void DisplayAllSalesManagers()
    {
        Console.WriteLine("\n--- All Sales Managers ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT SalesManager FROM Sales";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Sales Manager: {reader["SalesManager"]}");
            }
            reader.Close();
        }
    }

    static void DisplayProductsWithMaxQuantity()
    {
        Console.WriteLine("\n--- Products with Maximum Quantity ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Products WHERE Quantity = (SELECT MAX(Quantity) FROM Products)";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Quantity: {reader["Quantity"]}, CostPrice: {reader["CostPrice"]}");
            }
            reader.Close();
        }
    }

    static void DisplayProductsWithMinQuantity()
    {
        Console.WriteLine("\n--- Products with Minimum Quantity ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Products WHERE Quantity = (SELECT MIN(Quantity) FROM Products)";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Quantity: {reader["Quantity"]}, CostPrice: {reader["CostPrice"]}");
            }
            reader.Close();
        }
    }

    static void DisplayProductsWithMinCostPrice()
    {
        Console.WriteLine("\n--- Products with Minimum Cost Price ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Products WHERE CostPrice = (SELECT MIN(CostPrice) FROM Products)";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Quantity: {reader["Quantity"]}, CostPrice: {reader["CostPrice"]}");
            }
            reader.Close();
        }
    }

    static void DisplayProductsWithMaxCostPrice()
    {
        Console.WriteLine("\n--- Products with Maximum Cost Price ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Products WHERE CostPrice = (SELECT MAX(CostPrice) FROM Products)";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Quantity: {reader["Quantity"]}, CostPrice: {reader["CostPrice"]}");
            }
            reader.Close();
        }
    }

    static void DisplayProductsByType(string type)
    {
        Console.WriteLine($"\n--- Products of type '{type}' ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = $"SELECT * FROM Products WHERE Type = '{type}'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Name: {reader["Name"]}, Type: {reader["Type"]}, Quantity: {reader["Quantity"]}, CostPrice: {reader["CostPrice"]}");
            }
            reader.Close();
        }
    }


    static void DisplayProductsSoldByManager(string manager)
    {
        Console.WriteLine($"\n--- Products sold by manager '{manager}' ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = $"SELECT * FROM Sales WHERE SalesManager = '{manager}'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Customer: {reader["CustomerName"]}, Quantity Sold: {reader["QuantitySold"]}, Unit Price: {reader["UnitPrice"]}, Sale Date: {reader["SaleDate"]}");
            }
            reader.Close();
        }
    }


    static void DisplayProductsPurchasedByCompany(string companyName)
    {
        Console.WriteLine($"\n--- Products purchased by company '{companyName}' ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = $"SELECT * FROM Sales WHERE CustomerName = '{companyName}'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ProductID: {reader["ProductID"]}, Sales Manager: {reader["SalesManager"]}, Quantity Sold: {reader["QuantitySold"]}, Unit Price: {reader["UnitPrice"]}, Sale Date: {reader["SaleDate"]}");
            }
            reader.Close();
        }
    }


    static void DisplayRecentSalesInfo()
    {
        Console.WriteLine("\n--- Recent Sales Information ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 10 * FROM Sales ORDER BY SaleDate DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"SaleID: {reader["SaleID"]}, Customer: {reader["CustomerName"]}, Sales Manager: {reader["SalesManager"]}, Quantity Sold: {reader["QuantitySold"]}, Unit Price: {reader["UnitPrice"]}, Sale Date: {reader["SaleDate"]}");
            }
            reader.Close();
        }
    }


    static void DisplayAverageQuantityByProductType()
    {
        Console.WriteLine("\n--- Average Quantity by Product Type ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Type, AVG(Quantity) AS AverageQuantity FROM Products GROUP BY Type";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Type: {reader["Type"]}, Average Quantity: {reader["AverageQuantity"]}");
            }
            reader.Close();
        }
    }

    static void InsertNewProduct(string name, string type, int quantity, decimal costPrice, string supplierName, string supplierContact)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Products (Name, Type, Quantity, CostPrice, SupplierName, SupplierContact) VALUES (@Name, @Type, @Quantity, @CostPrice, @SupplierName, @SupplierContact)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@CostPrice", costPrice); 
            command.Parameters.AddWithValue("@SupplierName", supplierName);
            command.Parameters.AddWithValue("@SupplierContact", supplierContact);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows inserted into Products table.");
        }
    }

    static void InsertNewProductType(string typeName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO ProductTypes (TypeName) VALUES (@TypeName)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TypeName", typeName);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows inserted into ProductTypes table.");
        }
    }

    static void InsertNewSalesManager(string managerName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO SalesManagers (ManagerName) VALUES (@ManagerName)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ManagerName", managerName);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows inserted into SalesManagers table.");
        }
    }

    static void InsertNewCustomerCompany(string companyName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO CustomerCompanies (CompanyName) VALUES (@CompanyName)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CompanyName", companyName);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows inserted into CustomerCompanies table.");
        }
    }

    
    static void UpdateExistingProduct(int productId, string name, string type, int quantity, decimal costPrice, string supplierName, string supplierContact)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Products SET Name = @Name, Type = @Type, Quantity = @Quantity, CostPrice = @CostPrice, SupplierName = @SupplierName, SupplierContact = @SupplierContact WHERE ProductID = @ProductID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductID", productId);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@CostPrice", costPrice);
            command.Parameters.AddWithValue("@SupplierName", supplierName);
            command.Parameters.AddWithValue("@SupplierContact", supplierContact);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows updated in Products table.");
        }
    }

    
    static void UpdateExistingCustomerCompany(int companyId, string companyName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE CustomerCompanies SET CompanyName = @CompanyName WHERE CompanyID = @CompanyID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CompanyID", companyId);
            command.Parameters.AddWithValue("@CompanyName", companyName);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows updated in CustomerCompanies table.");
        }
    }

    
    static void UpdateExistingSalesManager(int managerId, string managerName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE SalesManagers SET ManagerName = @ManagerName WHERE ManagerID = @ManagerID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ManagerID", managerId);
            command.Parameters.AddWithValue("@ManagerName", managerName);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows updated in SalesManagers table.");
        }
    }

    
    static void UpdateExistingProductType(int typeId, string typeName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE ProductTypes SET TypeName = @TypeName WHERE TypeID = @TypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TypeID", typeId);
            command.Parameters.AddWithValue("@TypeName", typeName);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows updated in ProductTypes table.");
        }
    }

    
    static void DeleteProduct(int productId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            
            string deleteSalesQuery = "DELETE FROM Sales WHERE ProductID = @ProductID";
            SqlCommand deleteSalesCommand = new SqlCommand(deleteSalesQuery, connection);
            deleteSalesCommand.Parameters.AddWithValue("@ProductID", productId);
            int rowsAffectedSales = deleteSalesCommand.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffectedSales} rows deleted from Sales table.");

            
            string deleteProductQuery = "DELETE FROM Products WHERE ProductID = @ProductID";
            SqlCommand deleteProductCommand = new SqlCommand(deleteProductQuery, connection);
            deleteProductCommand.Parameters.AddWithValue("@ProductID", productId);
            int rowsAffectedProduct = deleteProductCommand.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffectedProduct} rows deleted from Products table.");
        }
    }


    
    static void DeleteSalesManager(int managerId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM SalesManagers WHERE ManagerID = @ManagerID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ManagerID", managerId);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows deleted from SalesManagers table.");
        }
    }

   
    static void DeleteProductType(int typeId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM ProductTypes WHERE TypeID = @TypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TypeID", typeId);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows deleted from ProductTypes table.");
        }
    }

    
    static void DeleteCustomerCompany(int companyId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM CustomerCompanies WHERE CompanyID = @CompanyID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CompanyID", companyId);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} rows deleted from CustomerCompanies table.");
        }
    }

    static void DisplayManagerWithMostSales()
    {
        Console.WriteLine("\n--- Manager with Most Sales by Quantity ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 SalesManager, SUM(QuantitySold) AS TotalSales FROM Sales GROUP BY SalesManager ORDER BY TotalSales DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Sales Manager: {reader["SalesManager"]}, Total Sales: {reader["TotalSales"]}");
            }
            reader.Close();
        }
    }

    static void DisplayManagerWithHighestProfit()
    {
        Console.WriteLine("\n--- Manager with Highest Profit ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 SalesManager, SUM(QuantitySold * UnitPrice) AS TotalProfit FROM Sales GROUP BY SalesManager ORDER BY TotalProfit DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Sales Manager: {reader["SalesManager"]}, Total Profit: {reader["TotalProfit"]}");
            }
            reader.Close();
        }
    }

    static void DisplayManagerWithHighestProfitInTimeFrame(DateTime startDate, DateTime endDate)
    {
        Console.WriteLine($"\n--- Manager with Highest Profit from {startDate.ToShortDateString()} to {endDate.ToShortDateString()} ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 SalesManager, SUM(QuantitySold * UnitPrice) AS TotalProfit " +
                           "FROM Sales " +
                           "WHERE SaleDate BETWEEN @StartDate AND @EndDate " +
                           "GROUP BY SalesManager " +
                           "ORDER BY TotalProfit DESC";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate", endDate);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Sales Manager: {reader["SalesManager"]}, Total Profit: {reader["TotalProfit"]}");
            }
            reader.Close();
        }
    }

    static void DisplayCompanyWithHighestPurchaseAmount()
    {
        Console.WriteLine("\n--- Company with Highest Purchase Amount ---");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 CustomerName, SUM(QuantitySold * UnitPrice) AS TotalPurchaseAmount FROM Sales GROUP BY CustomerName ORDER BY TotalPurchaseAmount DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Customer Name: {reader["CustomerName"]}, Total Purchase Amount: {reader["TotalPurchaseAmount"]}");
            }
            reader.Close();
        }
    }

}
