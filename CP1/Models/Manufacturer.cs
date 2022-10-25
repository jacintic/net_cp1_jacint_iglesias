using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1.Models;

public class Manufacturer
{
    // Attributes
    private long Id;
    private string Name { get; set; }
    // Methdos
    // ToString
    public override string ToString()
    {
        return 
            "Manufacturer:\n" +
            $".Name: {Name}";
    }
}
