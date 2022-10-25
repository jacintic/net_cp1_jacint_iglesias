using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1.Models;

public class Product
{
    // Attributes
    private long Id;
    private string Name { get; set; }
    private double Weight { get; set; }
    private double Price { get; set; }
    private int Stock { get; set; }
    private double Cost { get; set; }
    private DateTime CreatedAt { get; set; }
    // Class Associations
    private Manufacturer manufacturer { get; set; }
    // Methods
    // ToString
    public override string ToString()
    {
        return
            $"Product:\n" + 
            ".Name: {Name} " +
            $".Weight: {Weight} " +
            $".Price: {Price} " +
            $".Stock: {Stock} " +
            $".Cost: {Cost} " +
            $".CreatedAt: {CreatedAt} " +
            $"\n{manufacturer} ";
    }
}
