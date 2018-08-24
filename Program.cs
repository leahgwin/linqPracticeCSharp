using System;
using System.Linq;
using System.Collections.Generic;

// Define a bank
public class Bank
{
    public string Symbol { get; set; }
    public string Name { get; set; }
}

// Define a customer
public class Customer
{
    public string Name { get; set; }
    public double Balance { get; set; }
    public string Bank { get; set; }
}

public class GroupedMillionaires
{
    public string Bank { get; set; }
    public IEnumerable<string> Millionaires { get; set; }
}

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            //FILTERING-----------------------------------------------------------------

            // Find the words in the collection that start with the letter 'L'
            List<string> fruits = new List<string>() { "Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry" };

            IEnumerable<string> LFruits = from fruit in fruits
                                          where fruit.StartsWith("L")
                                          orderby fruit ascending
                                          select fruit;

            foreach (string fruit in LFruits)
            {
                Console.WriteLine($"{fruit}");
            }

            // Which of the following numbers are multiples of 4 or 6
            List<int> numbersMultiples = new List<int>()
{
    15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
};

            List<int> fourSixMultiples = numbersMultiples.Where(n => n % 4 == 0 || n % 6 == 0).ToList();
            foreach (int number in fourSixMultiples)
            {
                Console.WriteLine(number);
            }

            //ORDERING-----------------------------------------------------------------

            // Order these student names alphabetically, in descending order (Z to A)
            List<string> names = new List<string>()
                {
                    "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
                    "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
                    "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
                    "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
                    "Francisco", "Tre"
                };

            var descend = from name in names
                          orderby name descending
                          select name;

            foreach (string name in descend)
            {
                Console.WriteLine($"{name}");
            }

            // Build a collection of these numbers sorted in ascending order
            List<int> numbers = new List<int>()
                {
                    15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
                };

            var descendNum = from num in numbers
                             orderby num ascending
                             select num;

            foreach (int num in descendNum)
            {
                Console.WriteLine($"{num}");
            }

            //AGGREGATE-----------------------------------------------------------------

            // Output how many numbers are in this list
            List<int> numbersOutput = new List<int>()
                {
                    15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
                };

            Console.WriteLine($"There are {numbersOutput.Count()} numbers in this list.");

            // How much money have we made?
            List<double> purchases = new List<double>()
                {
                    2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
                };

            var sumPurchases = purchases.Sum();
            Console.WriteLine($"We've made {sumPurchases.ToString("C")}.");

            // What is our most expensive product?
            List<double> prices = new List<double>()
                {
                    879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
                };
            Console.WriteLine(prices.Max().ToString("C"));

            //PARTITIONING-----------------------------------------------------------------

            /*
                Store each number in the following List until a perfect square
                is detected.

                Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
            */
            List<int> wheresSquaredo = new List<int>()
                    {
                        66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
                    };

            List<int> nonSquares = wheresSquaredo.TakeWhile(n => Math.Sqrt(n) % 1 != 0).ToList();
            foreach (var item in nonSquares)
            {
                Console.WriteLine(item.ToString());
            }

            //CUSTOM TYPES/ GROUPING-----------------------------------------------------------------

            //(this is steve's code for the groupings/joining that I'm playing around with)

            // Build a collection of customers who are millionaires
            //     public class Customer


            List<Customer> customers = new List<Customer>() {
                new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
                new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
                new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
                new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
                new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
                new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
                new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
                new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
                new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
                new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
            };

            /*
                Given the same customer set, display how many millionaires per bank.
                Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq
            */

            var groupedByBank = customers.Where(c => c.Balance >= 1000000).GroupBy(
                p => p.Bank,  // Group banks
                p => p.Name,  // by millionaire names
                (bank, millionaires) => new GroupedMillionaires()
                {
                    Bank = bank,
                    Millionaires = millionaires
                }
            ).ToList();

            foreach (var item in groupedByBank)
            {
                Console.WriteLine($"{item.Bank}: {string.Join(" and ", item.Millionaires)}");
            }

            // Create some banks and store in a List
            List<Bank> banks = new List<Bank>() {
                new Bank(){ Name="First Tennessee", Symbol="FTB"},
                new Bank(){ Name="Wells Fargo", Symbol="WF"},
                new Bank(){ Name="Bank of America", Symbol="BOA"},
                new Bank(){ Name="Citibank", Symbol="CITI"},
            };

            List<Customer> millionaireReport = customers.Where(c => c.Balance >= 1000000)
                .Select(c => new Customer()
                {
                    Name = c.Name,
                    Bank = banks.Find(b => b.Symbol == c.Bank).Name,
                    Balance = c.Balance
                })
                .ToList();

            foreach (Customer customer in millionaireReport)
            {
                Console.WriteLine($"{customer.Name} at {customer.Bank}");
            }









        }
    }
}
