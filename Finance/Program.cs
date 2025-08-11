using System;
using System.Collections.Generic;

// Record for financial data
public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

// Interface for transaction processing
public interface ITransactionProcessor
{
    void Process(Transaction transaction);
}

// Concrete transaction processors
public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[Bank Transfer] Processed {transaction.Amount} for {transaction.Category}");
    }
}

public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[Mobile Money] Processed {transaction.Amount} for {transaction.Category}");
    }
}

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[Crypto Wallet] Processed {transaction.Amount} for {transaction.Category}");
    }
}

// Base account class
public class Account
{
    public string AccountNumber { get; }
    public decimal Balance { get; protected set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public virtual void ApplyTransaction(Transaction transaction)
    {
        Balance -= transaction.Amount;
        Console.WriteLine($"Transaction applied. New balance: {Balance}");
    }
}

// Sealed SavingsAccount
public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance) { }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
        }
        else
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied. Updated balance: {Balance}");
        }
    }
}

// Main application
public class FinanceApp
{
    private List<Transaction> _transactions = new List<Transaction>();

    public void Run()
    {
        // Create account
        var account = new SavingsAccount("ACC123", 1000);

        // Create transactions
        var t1 = new Transaction(1, DateTime.Now, 150, "Groceries");
        var t2 = new Transaction(2, DateTime.Now, 200, "Utilities");
        var t3 = new Transaction(3, DateTime.Now, 50, "Entertainment");

        // Processors
        ITransactionProcessor p1 = new MobileMoneyProcessor();
        ITransactionProcessor p2 = new BankTransferProcessor();
        ITransactionProcessor p3 = new CryptoWalletProcessor();

        // Process transactions
        p1.Process(t1);
        account.ApplyTransaction(t1);

        p2.Process(t2);
        account.ApplyTransaction(t2);

        p3.Process(t3);
        account.ApplyTransaction(t3);

        // Save transactions
        _transactions.Add(t1);
        _transactions.Add(t2);
        _transactions.Add(t3);
    }
}

class Program
{
    static void Main()
    {
        var app = new FinanceApp();
        app.Run();
    }
}

