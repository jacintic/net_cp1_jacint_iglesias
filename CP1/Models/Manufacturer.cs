using CP1.Helpers;
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
    private static long NextManufacturer;
    internal string Name { get; set; }
    // Constructor
    public Manufacturer() 
    {
        Id = ++NextManufacturer;
    }

    public Manufacturer(List<Manufacturer> manufacturers)
    {
        Id = FindMaxId(manufacturers) + 1;
        if (NextManufacturer == (Id - 1))
            ++NextManufacturer;
    }
    // Getters and Setters
    internal long GetId()
    {
        return Id;
    }
    // Methdos
    public long FindMaxId(List<Manufacturer> manufacturers)
    {
        long maxId = 0;
        foreach (Manufacturer manuf in manufacturers)
            if (maxId < manuf.Id)
                maxId = manuf.Id;
        return maxId;
    }
    // ToString
    public override string ToString()
    {
        return 
            "Manufacturer:\n" +
            $"\t .{Name}";
    }
}
