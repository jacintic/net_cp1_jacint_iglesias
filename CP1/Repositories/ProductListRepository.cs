using CP1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CP1.Repositories;
public class ProductListRepository : IProductRepository
{
    // Attributes
    private List<Product> products;

    // Constructor
    public ProductListRepository()
    {
        products = new List<Product>();
    }

    // Getters & Setters
    public List<Product> Products
    {
        get => products;
        set => products = value;
    }
public void SetProducts(List<Product> products)
{
    this.products = products;
}

// Methods
public Product FindById(int id)
{
    // Handle empty list
    try
    {
        if (products.Count == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine("Invalid Operation Exception:");
        Console.WriteLine(e.Message);
        return null;
    }
    // Handle error by iterating within list
    try
    {
        foreach (Product p in products)
            if (p.GetId() == id)
                return p;
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception Type: " + e.GetType());
        Console.WriteLine(e.Message);
    }
    return null;
}
}
