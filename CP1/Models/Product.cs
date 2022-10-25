using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1.Models;

public class Product
{
    // Attributes
    private long Id = 1;
    private static int NextProductId = 1;
    internal string Name { get; set; }
    internal double Weight { get; set; }
    internal double Price { get; set; }
    internal int Stock { get; set; }
    internal double Cost { get; set; }
    internal DateTime CreatedAt { get; set; }
    // Class Associations
    internal Manufacturer manufacturer { get; set; }
    // Constructor
    public Product()
    {
        Id = NextProductId++;
        CreatedAt = GenerateRandomDate();
    }
    // Getter & Setters
    internal long GetId()
    {
        return Id;
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
    // ToString
    public override string ToString()
    {
        return
            $"Product:" +
            $"\n\t.Name: {Name} " +
            $"\n\t.Weight: {Weight} " +
            $"\n\t.Price: {Price} " +
            $"\n\t.Stock: {Stock} " +
            $"\n\t.Cost: {Cost} " +
            $"\n\t.CreatedAt: {CreatedAt} " +
            $"\n\t{manufacturer} \n";
    }
}
