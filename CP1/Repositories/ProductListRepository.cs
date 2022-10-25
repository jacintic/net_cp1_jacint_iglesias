using CP1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
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

    public List<Product> FindAll()
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
            return products;

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception Type: " + e.GetType());
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public List<Product> FindByPriceRange(double min, double max)
    {
        List<Product> filtered = null;
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
        // Handle instance of List error
        try
        {
            filtered = new List<Product>();
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: unexpected Exception");
            Console.WriteLine(e.Message);
            return null;
        }
        // Handle error by iterating within list
        try
        {
            foreach (Product p in products)
                if (p.Price >= min && p.Price <= max)
                    filtered.Add(p);
            return filtered;

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception Type: " + e.GetType());
            Console.WriteLine(e.Message);
        }
        return filtered;
    }

    
    public List<Product> FindByDateBefore(DateTime date)
    {
        List<Product> filtered = null;
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
        // Handle instance of List error
        try
        {
            filtered = new List<Product>();
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: unexpected Exception");
            Console.WriteLine(e.Message);
            return null;
        }
        // Handle error by iterating within list
        try
        {
            foreach (Product p in products)
                if (p.CreatedAt < date)
                    filtered.Add(p);
            return filtered;

        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: unexpected Exception");
            Console.WriteLine(e.Message);
        }
        return filtered;
    }

    public List<Product> FindByNameLike(string name)
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
        // Handle errors during the regex and list iteration operations
        try
        {
            string pattern = @".*(" + name.ToLower() + ").*";
            Regex rg = new Regex(pattern);
            List<Product> productsLike = new List<Product>();
            products.ForEach(p =>
            {
                MatchCollection matchedProducts = rg.Matches(p.Name.ToLower());
                if (matchedProducts.Count > 0)
                    productsLike.Add(p);
            });
            return productsLike;
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR: unexpected Exception");
            Console.WriteLine(e.Message);
        }
        return null;
        
    }

    // utilities
    public string PrintAllProducts()
    {
        return String.Join(" ", products);
    }

    public string PrintList(List<Product> list)
    {

        return "Printing List: \n" + String.Join(" ", list);
    }
}
