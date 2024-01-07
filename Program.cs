using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;

string greeting = @"Welcome to Thrown for a Loop
Your one-stop shop for used sporting equipment";

List<Product> products = new List<Product>()
{
   new Product()
    {
        Name = "Football",
        Price = 15.00M,
        Sold = true,
        Sport = "Football",
        StockDate = new DateTime(2023, 11, 20),
        ManufactureYear = 2010,
        Condition = 2.5
    },
    new Product()
    {
        Name = "Hockey Stick",
        Price = 12.00M,
        Sold = false,
        Sport = "Hockey",
        StockDate = new DateTime(2023, 12, 20),
        ManufactureYear = 2011,
        Condition = 4.1
    },
    new Product()
    {
        Name = "Cleats",
        Price = 5.00M,
        Sold = false,
        Sport = "Baseball",
        StockDate = new DateTime(2023, 10, 20),
        ManufactureYear = 2010,
        Condition = 4.8
    },
    new Product()
    {
        Name = "Glove",
        Price = 4.00M,
        Sold = false,
        Sport = "Baseball",
        StockDate = new DateTime(2022, 10, 22),
        ManufactureYear = 2009,
        Condition = 3.4
    },
    new Product()
    {
        Name = "Soccer Ball",
        Price = 3.00M,
        Sold = false,
        Sport = "Soccer",
        StockDate = new DateTime(2022, 9, 20),
        ManufactureYear = 2010,
        Condition = 1.7
    }
};

Console.WriteLine(greeting);
string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. View All Products
                        2. View Product Details
                        3. View Latest Products");
    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        ListProducts();
    }
    else if (choice == "2")
    {
        ViewProductDetails();
    }
    else if (choice == "3")
    {
        ViewLatestProducts();
    }
}

void ViewProductDetails()
{
    ListProducts();

    Product chosenProduct = null;
    while (chosenProduct == null)
    {
        Console.WriteLine("Please enter a product number: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integars!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }

    DateTime now = DateTime.Now;
    TimeSpan timeInStock = now - chosenProduct.StockDate;

    Console.WriteLine(@$"You chose: 
{chosenProduct.Name}, which costs {chosenProduct.Price} dollars.
It is {now.Year - chosenProduct.ManufactureYear} years old. 
It {(chosenProduct.Sold ? "is not available." : $"has been in stock for {timeInStock.Days} days.")}
The condition of this product is ranked {chosenProduct.Condition}/5");
}

void ListProducts()
{
    decimal totalValue = 0.0M;
    foreach (Product product in products)
    {
        if (!product.Sold)
        {
            totalValue += product.Price;
        }
    }
    Console.WriteLine($"Total inventory value: ${totalValue}");
    Console.WriteLine("Products:");
    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {products[i].Name}");
    }
}

void ViewLatestProducts()
{
    List<Product> latestProducts = new List<Product>();

    DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);

    foreach (Product product in products)
    {
        if (product.StockDate > threeMonthsAgo && !product.Sold)
        {
            latestProducts.Add(product);
        }
    }

    for (int i = 0;i < latestProducts.Count; i++) 
    {
        Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
    }
}