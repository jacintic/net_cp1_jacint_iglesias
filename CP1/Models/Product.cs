using CP1.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace CP1.Models;

public class Product : IPrototype<Product>
{
    // Attributes
    private long Id = 1;
    private static long NextProductId;
    internal string Name { get; set; }
    internal double Weight { get; set; }
    private double Price { get; set; }
    internal int Stock { get; set; }
    internal double Cost { get; set; }
    internal DateTime CreatedAt { get; set; }
    // Class Associations
    internal Manufacturer manufacturer { get; set; }
    // Constructor
    public Product()
    {
        Id = ++NextProductId;
        CreatedAt = GenerateRandomDate();
    }

    public Product(List<Product> prodList)
    {
        if (prodList != null && prodList.Count > 0)
        {
            Id = FindMaxId(prodList) + 1;
            if (NextProductId == (Id - 1))
                ++NextProductId;
            CreatedAt = GenerateRandomDate();
        } else
        {
            Id = ++NextProductId;
            CreatedAt = GenerateRandomDate();
        }
    }
    // Getter & Setters
    internal long GetId()
    {
        return Id;
    }
    internal double GetPrice()
    {
        return Price;
    }
    internal void SetPrice(double price)
    {
        Price = price;
    }
    // Methods
    public DateTime GenerateRandomDate()
    {
        try
        {
        DateTime RandomDate;
        var thTH = new System.Globalization.CultureInfo("es-ES");
        Random rnd = new Random();
        int switchKey =  new Random().Next(1, 3);
        TimeSpan minutesTimespan = new TimeSpan(0, new Random().Next(1,60), 0);
        if (switchKey > 1)
            return DateTime.Now.Add(minutesTimespan);
        return DateTime.Now.Subtract(minutesTimespan);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: Something went wrong while generating the random DateTime");
            Console.WriteLine(e.Message);
        }
        return DateTime.Now;
    }

    // Helpers
    public long FindMaxId(List<Product> prodList)
    {
        long maxId = 0;
        foreach (Product prod in prodList)
            if (maxId < prod.Id)
                maxId = prod.Id;
        return maxId;
    }

    // ToString
    public override string ToString()
    {
        return
            $"Product:" +
            $"\n\t.Name: {Name} " +
            $"\n\t.Weight: {Weight} " +
            $"\n\t.Price: {Math.Round(Price, 2)}€" +
            $"\n\t.Stock: {Stock} " +
            $"\n\t.Cost: {Cost} " +
            $"\n\t.CreatedAt: {CreatedAt} " +
            $"\n\t{manufacturer} \n";
    }

    public override bool Equals(Object? prod)
    {
         if ((prod == null) || !this.GetType().Equals(prod.GetType()))
        {
            return false;
        }
        else
        {
            Product p = (Product)prod;
            return p.GetId() == Id || p.Name == Name;
        }
    }
    // clone product
    // param products is used to find a proper Id for the product within the list
    public Product CreateDeepCopy(List<Product> products)
    {
        var product = (Product)MemberwiseClone();
        product.Id = FindMaxId(products) + 1;
        if (NextProductId == (Id - 1))
            ++NextProductId;
        return product;
    }
}
