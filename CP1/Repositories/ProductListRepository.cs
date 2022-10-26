﻿using CP1.Models;
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
            if (Count() == 0)
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
            if (Count() == 0)
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
            if (Count() == 0)
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
                if (p.GetPrice() >= min && p.GetPrice() <= max)
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
            if (Count() == 0)
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
            if (Count() == 0)
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

    public List<Product> FindByManufacturerNameLike(string manufacturerName)
    {
        // Handle empty list
        try
        {
            if (Count() == 0)
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
            string pattern = @".*(" + manufacturerName.ToLower() + ").*";
            Regex rg = new Regex(pattern);
            List<Product> productsLike = new List<Product>();
            products.ForEach(p =>
            {
                MatchCollection matchedProducts = rg.Matches(p.manufacturer.Name.ToLower());
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

    public bool Save(Product product, Manufacturer manufacturer)
    {
        try
        {
            if (AlreadyExists(product, manufacturer))
                throw new DuplicateWaitObjectException("Duplicate Product. Aborting operation.");
            if (!Validate(product, manufacturer))
                throw new InvalidOperationException("Product is not valid.");
        }
        catch (DuplicateWaitObjectException e)
        {
            Console.WriteLine("Duplicate Wait Object Exception:");
            Console.WriteLine(e.Message);
            return false;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Invalid Operation Exception:");
            Console.WriteLine(e.Message);
            return false;
        }
        try
        {
            product.manufacturer = manufacturer;
            products.Add(product);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected exception when Saving the Product");
            Console.WriteLine(ex.Message);
            return false;
        }
        return false;
    }

    // utilities

    public bool AlreadyExists(Product product, Manufacturer manufacturer)
    {

        try
        {
            if (Count() == 0)
                throw new InvalidOperationException("Lists is empty, product can't be duplicate.");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Invalid Operation Exception:");
            Console.WriteLine(e.Message);
            return false;
        }
        foreach (Product p in products)
        {
            if (p.Name.Equals(product.Name))
                return true;
        }
        return false;
    }

    // main product validation
    public bool Validate(Product product, Manufacturer manufacturer)
    {
        return
            ValidateName(product.Name) &&
            //ValidateWeight(product.Weight) &&
            //ValidatePriceAndCost(product.GetPrice(), product.Cost) &&
            ValidateCreatedAt(product.CreatedAt) &&
            ValidateManufacturer(manufacturer);
    }

    // // -- validate sub-methods
    public bool ValidateName(string name)
    {
        return name != null && name.Length > 3;
    }
    public bool ValidateWeight(double weight)
    {
        return weight > 0 && weight < 50;
    }
    public bool ValidatePriceAndCost(double price, double cost)
    {
        return price > 350 && cost > 150 && price > cost;
    }
    public bool ValidateCreatedAt(DateTime date)
    {
        return date >= DateTime.MinValue && date <= DateTime.MaxValue && date < DateTime.Now.AddDays(1);
    }
    // NOTICE: move this method to Manufacturer ReopoList
    public bool ValidateManufacturer(Manufacturer manufacturer)
    {
        return
            manufacturer.Name != null &&
            manufacturer.Name.Length != 0 &&
            manufacturer.Name.Length >= 2 &&
            manufacturer.Name.Length <= 50;
    }
    // // -- end of validatesub- methods

    // Count products list
    public long Count()
    {
        return products.Count();
    }
    public string PrintAllProducts()
    {
        return String.Join(" ", products);
    }

    public string PrintList(List<Product> list)
    {
        try
        {
            if (Count() == 0)
                throw new InvalidOperationException("0 Elements. Lists is empty.");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Invalid Operation Exception:");
            Console.WriteLine(e.Message);
            return null;
        }
        return "Printing List: \n" + String.Join(" ", list);
    }
}
