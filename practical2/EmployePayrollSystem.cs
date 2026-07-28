using System;

// Interface
// Defines a contract that every employee must implement.
interface IPayable
{
    double CalculateSalary();
}

// Base Class (Parent)
class Employee
{
    public int Id;
    public string Name;

    // Constructor
    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }

    // Virtual Method (can be overridden)
    public virtual void Display()
    {
        Console.WriteLine("Employee ID   : " + Id);
        Console.WriteLine("Employee Name : " + Name);
    }
}

// Derived Class 1 (Full-Time Employee)
// Inherits Employee class and implements IPayable interface.
class FullTimeEmployee : Employee, IPayable
{
    public double MonthlySalary;

    public FullTimeEmployee(int id, string name, double salary)
        : base(id, name)
    {
        MonthlySalary = salary;
    }

    // Interface Method
    public double CalculateSalary()
    {
        return MonthlySalary;
    }

    // Method Overriding (Polymorphism)
    public override void Display()
    {
        base.Display();
        Console.WriteLine("Employee Type : Full-Time");
        Console.WriteLine("Salary        : $" + CalculateSalary());
    }
}

// Derived Class 2 (Part-Time Employee)
class PartTimeEmployee : Employee, IPayable
{
    public double HoursWorked;
    public double HourlyRate;

    public PartTimeEmployee(int id, string name, double hours, double rate)
        : base(id, name)
    {
        HoursWorked = hours;
        HourlyRate = rate;
    }

    // Interface Method
    public double CalculateSalary()
    {
        return HoursWorked * HourlyRate;
    }

    // Method Overriding (Polymorphism)
    public override void Display()
    {
        base.Display();
        Console.WriteLine("Employee Type : Part-Time");
        Console.WriteLine("Hours Worked  : " + HoursWorked);
        Console.WriteLine("Hourly Rate   : $" + HourlyRate);
        Console.WriteLine("Salary        : $" + CalculateSalary());
    }
}

// Main Class
class Program
{
    static void Main(string[] args)
    {
        // Creating objects using Parent Class reference
        Employee emp1 = new FullTimeEmployee(101, "Nishant", 50000);
        Employee emp2 = new PartTimeEmployee(102, "Rahul", 80, 20);

        Console.WriteLine("===== Employee Payroll System =====\n");

        // Runtime Polymorphism
        emp1.Display();

        Console.WriteLine();

        emp2.Display();

        Console.ReadKey();
    }
}