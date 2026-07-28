using System;

namespace StudentAdmissionManagement
{
    class Student
    {
        // Private Data Members
        private string name;
        private int age;
        private string gender;
        private string address;
        private double percentage;
        private string course;
        private string duration;
        private double fees;
        private double discount;
        private double finalFees;

        // Constructor
        public Student(string name, int age, string gender, string address, double percentage)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.address = address;
            this.percentage = percentage;
        }

        // Eligibility Method
        public bool IsEligible()
        {
            return percentage >= 50;
        }

        // Course Selection
        public void SelectCourse()
        {
            Console.WriteLine("\n========== AVAILABLE COURSES ==========");

            Console.WriteLine("1. Computer Engineering");
            Console.WriteLine("   Duration : 4 Years");
            Console.WriteLine("   Fees     : Rs.100000");

            Console.WriteLine("\n2. Information Technology");
            Console.WriteLine("   Duration : 4 Years");
            Console.WriteLine("   Fees     : Rs.90000");

            Console.WriteLine("\n3. Mechanical Engineering");
            Console.WriteLine("   Duration : 4 Years");
            Console.WriteLine("   Fees     : Rs.80000");

            Console.WriteLine("\n4. Civil Engineering");
            Console.WriteLine("   Duration : 4 Years");
            Console.WriteLine("   Fees     : Rs.75000");

            Console.Write("\nSelect Course (1-4): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    course = "Computer Engineering";
                    duration = "4 Years";
                    fees = 100000;
                    break;

                case 2:
                    course = "Information Technology";
                    duration = "4 Years";
                    fees = 90000;
                    break;

                case 3:
                    course = "Mechanical Engineering";
                    duration = "4 Years";
                    fees = 80000;
                    break;

                case 4:
                    course = "Civil Engineering";
                    duration = "4 Years";
                    fees = 75000;
                    break;

                default:
                    Console.WriteLine("Invalid Choice!");
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine("\nCourse Selected : " + course);
            Console.WriteLine("Course Duration : " + duration);
            Console.WriteLine("Course Fees     : Rs." + fees);

            CalculateDiscount();
        }

        // Discount Calculation
        public void CalculateDiscount()
        {
            if (percentage >= 90)
                discount = fees * 0.30;
            else if (percentage >= 80)
                discount = fees * 0.20;
            else if (percentage >= 70)
                discount = fees * 0.10;
            else if (percentage >= 50)
                discount = fees * 0.05;
            else
                discount = 0;

            finalFees = fees - discount;
        }

        // Registration Details
        public void DisplayDetails()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("     STUDENT REGISTRATION DETAILS");
            Console.WriteLine("==========================================");

            Console.WriteLine("Student Name       : " + name);
            Console.WriteLine("Age                : " + age);
            Console.WriteLine("Gender             : " + gender);
            Console.WriteLine("Address            : " + address);
            Console.WriteLine("12th Percentage    : " + percentage + "%");
            Console.WriteLine("Selected Course    : " + course);
            Console.WriteLine("Course Duration    : " + duration);
            Console.WriteLine("Course Fees        : Rs." + fees);
            Console.WriteLine("Scholarship        : Rs." + discount);
            Console.WriteLine("Final Fees         : Rs." + finalFees);

            Console.WriteLine("\nRegistration Status : SUCCESS");
            Console.WriteLine("Congratulations! Your admission has been completed successfully.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("     STUDENT ADMISSION MANAGEMENT SYSTEM");
            Console.WriteLine("===========================================");

            Console.Write("\nEnter Student Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Age          : ");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Gender       : ");
            string gender = Console.ReadLine();

            Console.Write("Enter Address      : ");
            string address = Console.ReadLine();

            Console.Write("Enter 12th Percentage : ");
            double percentage = Convert.ToDouble(Console.ReadLine());

            // Object Creation
            Student s1 = new Student(name, age, gender, address, percentage);

            // Eligibility Check
            if (!s1.IsEligible())
            {
                Console.WriteLine("\nSorry! You are not eligible for admission.");
                Console.WriteLine("Minimum 50% marks in 12th are required.");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCongratulations! You are eligible for admission.");

            // Course Selection
            s1.SelectCourse();

            Console.Write("\nPress Enter to Submit Registration...");
            Console.ReadLine();

            // Display Final Details
            s1.DisplayDetails();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}