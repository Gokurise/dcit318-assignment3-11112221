using System;
using System.Collections.Generic;
using System.IO;

// Interface for inventory entity
public interface IInventoryEntity
{
    string ItemId { get; }
    string ItemName { get; }
    int ItemQuantity { get; }
}

// Record type for InventoryItem
public record InventoryItem(string ItemId, string ItemName, int ItemQuantity) : IInventoryEntity;

// Generic Inventory Logger
public class InventoryLogger<T> where T : IInventoryEntity
{
    private List<T> inventoryList = new List<T>();

    public void AddInventoryItem(T newItem)
    {
        inventoryList.Add(newItem);
    }

    public void SaveInventoryToFile(string fileName)
    {
        using (var writer = new StreamWriter(fileName))
        {
            foreach (var item in inventoryList)
            {
                writer.WriteLine($"{item.ItemId},{item.ItemName},{item.ItemQuantity}");
            }
        }
    }

    public List<T> LoadInventoryFromFile(Func<string[], T> createInventoryItem, string fileName)
    {
        var loadedInventory = new List<T>();
        foreach (var line in File.ReadAllLines(fileName))
        {
            var parts = line.Split(',');
            loadedInventory.Add(createInventoryItem(parts));
        }
        return loadedInventory;
    }

    public void PrintInventory()
    {
        foreach (var item in inventoryList)
        {
            Console.WriteLine($"{item.ItemId} - {item.ItemName} - Qty: {item.ItemQuantity}");
        }
    }
}

// Main Inventory Application
public class InventoryApplication
{
    private InventoryLogger<InventoryItem> inventoryLogger = new InventoryLogger<InventoryItem>();

    public void Execute()
    {
        // Add sample inventory
        inventoryLogger.AddInventoryItem(new InventoryItem("ITM1", "Macbook", 5));
        inventoryLogger.AddInventoryItem(new InventoryItem("ITM2", "iPhone", 10));
        inventoryLogger.AddInventoryItem(new InventoryItem("ITM3", "Airpod", 3));

        // Save to file
        string inventoryFile = "inventory_data.txt";
        inventoryLogger.SaveInventoryToFile(inventoryFile);
        Console.WriteLine($"Inventory saved to {inventoryFile}.");

        // Load from file
        var loadedInventory = inventoryLogger.LoadInventoryFromFile(
            parts => new InventoryItem(parts[0], parts[1], int.Parse(parts[2])),
            inventoryFile
        );

        Console.WriteLine("\nLoaded Inventory:");
        foreach (var item in loadedInventory)
        {
            Console.WriteLine($"{item.ItemId} - {item.ItemName} - Qty: {item.ItemQuantity}");
        }
    }
}

class Program
{
    static void Main()
    {
        var inventoryApp = new InventoryApplication();
        inventoryApp.Execute();
    }
}

