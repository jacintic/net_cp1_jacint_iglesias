using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1.Models;

public class Manufacturer
{
    // Attributes
    private long Id = 1;
    private static int NextManufacturer = 1;
    internal string Name { get; set; }
    // Constructor
    public Manufacturer() 
    {
        Id = NextManufacturer++;
    }
    // Methdos
    // ToString
    public override string ToString()
    {
        return 
            "Manufacturer:\n" +
            $"\t .{Name}";
    }
}
