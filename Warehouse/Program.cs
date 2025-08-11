using System;
using System.Collections.Generic;

// Interface for products
public interface IProductItem
{
    string ProductId { get; }
    string ProductName { get; }
    int ProductQuantity { get; set; }
}

// Product types
public class ElectronicProduct : IProductItem
{
    public string ProductId { get; }
    public string ProductName { get; }
    public int ProductQuantity { get; set; }

    public ElectronicProduct(string productId, string productName, int productQuantity)
    {
        ProductId = productId;
        ProductName = productName;
        ProductQuantity = productQuantity;
    }
}

public class GroceryProduct : IProductItem
{
    public string ProductId { get; }
    public string ProductName { get; }
    public int ProductQuantity { get; set; }

    public GroceryProduct(string productId, string productName, int productQuantity)
    {
        ProductId = productId;
        ProductName = productName;
        ProductQuantity = productQuantity;
    }
}

// Generic repository
public class WarehouseRepository<T> where T : IProductItem
{
    private Dictionary<string, T> warehouseItems = new Dictionary<string, T>();

    public void AddProduct(T product)
    {
        if (warehouseItems.ContainsKey(product.ProductId))
            throw new Exception($"Product with ID {product.ProductId} already exists.");
        warehouseItems[product.ProductId] = product;
    }

    public void UpdateProductQuantity(string productId, int newQuantity)
    {
        if (!warehouseItems.ContainsKey(productId))
            throw new Exception($"Product with ID {productId} not found.");
        warehouseItems[productId].ProductQuantity = newQuantity;
    }

    public IEnumerable<T> GetAllProducts()
    {
        return warehouseItems.Values;
    }
}

// Manager class
public class WarehouseManager
{
    private WarehouseRepository<IProductItem> warehouseRepository = new WarehouseRepository<IProductItem>();

    public void LoadSampleProducts()
    {
        warehouseRepository.AddProduct(new ElectronicProduct("P001", "Laptop", 10));
        warehouseRepository.AddProduct(new GroceryProduct("P002", "Wheat", 50));
    }

    public void ExecuteWarehouseSystem()
    {
        LoadSampleProducts();

        try
        {
            warehouseRepository.UpdateProductQuantity("P001", 8);
            warehouseRepository.UpdateProductQuantity("P003", 20); // Will trigger exception
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("\nWarehouse Products:");
        foreach (var product in warehouseRepository.GetAllProducts())
        {
            Console.WriteLine($"{product.ProductId} - {product.ProductName} - Qty: {product.ProductQuantity}");
        }
    }
}

class Program
{
    static void Main()
    {
        var warehouseApp = new WarehouseManager();
        warehouseApp.ExecuteWarehouseSystem();
    }
}
