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
    List<Product> products = new List<Product>();

    // Constructor
    public ProductListRepository()
    {
        products = new List<Product>
        {
            new Product { Name = "MacBook Pro", Price = 16},
            new Product { Name = "MSI Modern", Price = 32},
            new Product { Name = "Asus A55A", Price = 8},
        };
    }

    // Methods
    public Product FindById(int id)
    {
        try
        {
            if(products.Count == 0)
                throw new InvalidOperationException("Lists is empty, can't find element.");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.GetType);
            Console.WriteLine(e.Message);
            return null;
        }
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
