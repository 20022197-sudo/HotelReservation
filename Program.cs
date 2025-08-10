using System;
using System.Collections.Generic;

namespace Hotel
{
    class Customer
    {
        public string Name { get; set; }
        public int NightsStayed { get; set; }
        public bool RoomService { get; set; }
        public double Cost { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\tWelcome to Sydney Hotel");

            List<Customer> customers = new List<Customer>();

            while (true)
            {
                Console.WriteLine("Enter Customer Name:");
                string name = Console.ReadLine();

                int nights = GetValidNumberOfNights();

                // Change 1: Room service input validation
                string roomServiceInput;
                do
                {
                    Console.WriteLine("Enter yes/no to indicate whether you want room service:");
                    roomServiceInput = Console.ReadLine().ToLower();

                    if (roomServiceInput != "yes" && roomServiceInput != "no")
                        Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
                while (roomServiceInput != "yes" && roomServiceInput != "no");

                bool roomService = roomServiceInput == "yes";

                double cost = CalculateTotalCost(nights, roomService);

                Customer customer = new Customer
                {
                    Name = name,
                    NightsStayed = nights,
                    RoomService = roomService,
                    Cost = cost
                };

                customers.Add(customer);

                Console.WriteLine($"Total price for {name} is ${cost:F2}");

                Console.WriteLine("________________________________________");
                Console.WriteLine("Press 'q' to quit or any other key to continue:");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "q")
                    break;

                Console.WriteLine("________________________________________");
            }

            // Change 2: Display average amount spent per customer
            if (customers.Count > 0)
            {
                double averageCost = 0;
                foreach (var customer in customers)
                    averageCost += customer.Cost;
                averageCost /= customers.Count;

                Console.WriteLine($"\nAverage amount spent per customer: ${averageCost:F2}");
            }
            else
            {
                Console.WriteLine("\nNo customers to calculate average cost.");
            }

            DisplaySummary(customers);
            DisplayHighestAndLowestSpenders(customers);
        }

        static int GetValidNumberOfNights()
        {
            int nights;
            while (true)
            {
                Console.WriteLine("Enter number of nights stayed (1-20):");
                if (int.TryParse(Console.ReadLine(), out nights) && nights >= 1 && nights <= 20)
                {
                    return nights;
                }
                Console.WriteLine("Invalid input. Please enter a number between 1 and 20.");
            }
        }

        static double CalculateTotalCost(int nights, bool roomService)
        {
            double cost;

            if (nights >= 1 && nights <= 3)
                cost = nights * 100;
            else if (nights >= 4 && nights <= 10)
                cost = nights * 80.5;
            else
                cost = nights * 75.3;

            if (roomService)
                cost += cost * 0.10;

            return cost;
        }

        static void DisplaySummary(List<Customer> customers)
        {
            Console.WriteLine("\n\t\t\tSummary of Reservations");
            Console.WriteLine("Name\t\tNights\t\tRoom Service\tCharge");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Name}\t\t{customer.NightsStayed}\t\t{(customer.RoomService ? "Yes" : "No")}\t\t${customer.Cost:F2}");
            }
        }

        static void DisplayHighestAndLowestSpenders(List<Customer> customers)
        {
            if (customers.Count == 0)
            {
                Console.WriteLine("No customer data available.");
                return;
            }

            Customer highest = customers[0];
            Customer lowest = customers[0];

            foreach (var customer in customers)
            {
                if (customer.Cost > highest.Cost)
                    highest = customer;
                if (customer.Cost < lowest.Cost)
                    lowest = customer;
            }

            Console.WriteLine($"\nCustomer spending the most is {highest.Name} with ${highest.Cost:F2}");
            Console.WriteLine($"Customer spending the least is {lowest.Name} with ${lowest.Cost:F2}");
        }
    }
}
