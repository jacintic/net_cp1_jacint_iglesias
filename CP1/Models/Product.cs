using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1.Models;

public class Product
{
    // Attributes
    private long Id = 1;  // set as private
    private static int NextProductId = 1; // set as private
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
    }
    // Getter & Setters
    internal long GetId()
    {
        return Id;
    }
    // Methods
    // ToString
    public override string ToString()
    {
        return
            $"Product:\n" +
            $".Name: {Name} " +
            $".Weight: {Weight} " +
            $".Price: {Price} " +
            $".Stock: {Stock} " +
            $".Cost: {Cost} " +
            $".CreatedAt: {CreatedAt} " +
            $"\n{manufacturer} ";
    }
}
