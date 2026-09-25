using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExpenseTrackerApp
{
    /// <summary>
    /// Distinguishes the two kinds of financial transaction the tracker supports.
    /// </summary>
    public enum TransactionType
    {
        Income,
        Expense
    }

    /// <summary>
    /// Represents a single financial transaction (income or expense).
    /// </summary>
    public sealed class Transaction
    {
        public TransactionType Type { get; }
        public double Amount { get; }
        public string Description { get; }
        public DateTime Timestamp { get; }

        public Transaction(TransactionType type, double amount, string description)
        {
            Type = type;
            Amount = amount;
            Description = string.IsNullOrWhiteSpace(description) ? "(no description)" : description.Trim();
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            string sign = Type == TransactionType.Income ? "+" : "-";
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Type,-8} {sign}{Amount:F2}  {Description}";
        }
    }

    /// <summary>
    /// Custom exception used for domain-specific validation failures
    /// (e.g. negative amounts, insufficient funds), as opposed to
    /// unexpected runtime errors.
    /// </summary>
    public sealed class TransactionException : Exception
    {
        public TransactionException(string message) : base(message) { }
    }

    /// <summary>
    /// Maintains the list of income sources the user has entered so far.
    /// No defaults are provided — every source is created by the user.
    /// </summary>
    public sealed class SourceManager
    {
        private readonly List<string> _sources = new List<string>();

        public IReadOnlyList<string> Sources => _sources.AsReadOnly();

        /// <summary>
        /// Adds a new source if it doesn't already exist (case-insensitive).
        /// Returns the stored (trimmed) source name.
        /// </summary>
        public string AddSource(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new TransactionException("Source name cannot be empty.");
            }

            string trimmed = name.Trim();
            string existing = _sources.Find(s => string.Equals(s, trimmed, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                return existing;
            }

            _sources.Add(trimmed);
            return trimmed;
        }
    }

    /// <summary>
    /// Core business logic for tracking a running balance and its
    /// transaction history. Contains no console I/O, so it can be
    /// unit-tested or reused with a different front end (GUI, web API, etc.).
    /// </summary>
    public sealed class ExpenseTracker
    {
        private readonly List<Transaction> _history = new List<Transaction>();

        public double Balance { get; private set; }

        public IReadOnlyList<Transaction> History => _history.AsReadOnly();

        public void AddIncome(double amount, string description)
        {
            if (amount <= 0)
            {
                throw new TransactionException("Income must be greater than 0.");
            }

            Balance += amount;
            _history.Add(new Transaction(TransactionType.Income, amount, description));
        }

        public void AddExpense(double amount, string description)
        {
            if (amount <= 0)
            {
                throw new TransactionException("Expense amount must be greater than 0.");
            }

            if (amount > Balance)
            {
                throw new TransactionException("Insufficient balance.");
            }

            Balance -= amount;
            _history.Add(new Transaction(TransactionType.Expense, amount, description));
        }

        /// <summary>
        /// Removes the transaction at the given zero-based index and reverses
        /// its effect on the balance (adds back an expense, subtracts an income).
        /// </summary>
        public void DeleteTransaction(int index)
        {
            if (index < 0 || index >= _history.Count)
            {
                throw new TransactionException("Invalid transaction number.");
            }

            Transaction removed = _history[index];

            if (removed.Type == TransactionType.Income)
            {
                Balance -= removed.Amount;
            }
            else
            {
                Balance += removed.Amount;
            }

            _history.RemoveAt(index);
        }
    }

    /// <summary>
    /// Console front end: handles all user interaction (menu, prompts,
    /// input parsing) and delegates business rules to ExpenseTracker.
    /// </summary>
    public static class Program
    {
        private enum MenuOption
        {
            AddIncome = 1,
            AddExpense = 2,
            CheckBalance = 3,
            ViewHistory = 4,
            DeleteTransaction = 5,
            Exit = 6
        }

        public static void Main()
        {
            var tracker = new ExpenseTracker();
            var sourceManager = new SourceManager();
            bool exit = false;

            while (!exit)
            {
                PrintMenu();

                if (!TryReadInt("Enter your choice: ", out int choiceValue))
                {
                    Console.WriteLine("Error: Please enter a valid menu option.");
                    continue;
                }

                switch ((MenuOption)choiceValue)
                {
                    case MenuOption.AddIncome:
                        HandleAddIncome(tracker, sourceManager);
                        break;
                    case MenuOption.AddExpense:
                        HandleAddExpense(tracker);
                        break;
                    case MenuOption.CheckBalance:
                        DisplayBalance(tracker);
                        break;
                    case MenuOption.ViewHistory:
                        DisplayHistory(tracker);
                        break;
                    case MenuOption.DeleteTransaction:
                        HandleDeleteTransaction(tracker);
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Thank you!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== Expense Tracking System =====");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Delete a Transaction");
            Console.WriteLine("6. Exit");
        }

        private static void HandleAddIncome(ExpenseTracker tracker, SourceManager sourceManager)
        {
            string source = ChooseOrCreateSource(sourceManager);
            if (source == null)
            {
                // User cancelled source entry.
                return;
            }

            if (!TryReadDouble($"Enter amount for '{source}': ", out double amount))
            {
                Console.WriteLine("Error: Please enter a valid number.");
                return;
            }

            try
            {
                tracker.AddIncome(amount, source);
                Console.WriteLine("Income added successfully.");
                Console.WriteLine($"Current Balance: {tracker.Balance:F2}");
            }
            catch (TransactionException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lets the user type any income source directly — no predefined
        /// options or suggestions are shown. This allows tracking multiple,
        /// fully user-defined income sources over time.
        /// </summary>
        private static string ChooseOrCreateSource(SourceManager sourceManager)
        {
            Console.Write("Enter income source: ");
            string input = Console.ReadLine();

            try
            {
                return sourceManager.AddSource(input);
            }
            catch (TransactionException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        private static void HandleAddExpense(ExpenseTracker tracker)
        {
            bool addAnother = true;

            while (addAnother)
            {
                Console.Write("Enter expense description: ");
                string description = Console.ReadLine();

                if (!TryReadDouble($"Enter amount for '{description}': ", out double amount))
                {
                    Console.WriteLine("Error: Please enter a valid number.");
                }
                else
                {
                    try
                    {
                        tracker.AddExpense(amount, description);
                        Console.WriteLine("Expense added successfully.");
                        Console.WriteLine($"Remaining Balance: {tracker.Balance:F2}");
                    }
                    catch (TransactionException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                Console.Write("Add another expense? (y/n): ");
                string response = Console.ReadLine();
                addAnother = !string.IsNullOrWhiteSpace(response) &&
                             response.Trim().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            }
        }

        private static void DisplayBalance(ExpenseTracker tracker)
        {
            Console.WriteLine($"Current Balance: {tracker.Balance:F2}");
        }

        private static void DisplayHistory(ExpenseTracker tracker)
        {
            if (tracker.History.Count == 0)
            {
                Console.WriteLine("No transactions recorded yet.");
                return;
            }

            Console.WriteLine("--- Transaction History ---");
            PrintNumberedHistory(tracker);
        }

        private static void HandleDeleteTransaction(ExpenseTracker tracker)
        {
            if (tracker.History.Count == 0)
            {
                Console.WriteLine("No transactions to delete.");
                return;
            }

            Console.WriteLine("--- Select a Transaction to Delete ---");
            PrintNumberedHistory(tracker);

            if (!TryReadInt("Enter the number of the transaction to delete: ", out int choice))
            {
                Console.WriteLine("Error: Please enter a valid number.");
                return;
            }

            try
            {
                tracker.DeleteTransaction(choice - 1); // convert to zero-based index
                Console.WriteLine("Transaction deleted successfully.");
                Console.WriteLine($"Updated Balance: {tracker.Balance:F2}");
            }
            catch (TransactionException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void PrintNumberedHistory(ExpenseTracker tracker)
        {
            IReadOnlyList<Transaction> history = tracker.History;
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {history[i]}");
            }
        }

        private static bool TryReadInt(string prompt, out int value)
        {
            Console.Write(prompt);
            return int.TryParse(Console.ReadLine(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
        }

        private static bool TryReadDouble(string prompt, out double value)
        {
            Console.Write(prompt);
            return double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }
    }
}