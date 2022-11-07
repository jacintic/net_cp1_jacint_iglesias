using CP1.Models;
using POO1;
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
    private ProductValidaitior ProductValidaitior;

    // Constructor
    public ProductListRepository()
    {
        products = new List<Product>();
        ProductValidaitior = new ProductValidaitior();
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
    public Product FindById(long id)
    {
        // Handle empty 
        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");
        foreach (Product p in products)
            if (p.GetId() == id)
                return p;
        // Handle Id not found in Repo
        throw new InvalidOperationException("ERROR: Id not found.");
    }

    public List<Product> FindAll()
    {
        // Handle empty list
        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");
        return products;

    }

    public List<Product> FindByPriceRange(double min, double max)
    {
        List<Product> filtered = null;
        // Handle empty list
        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");
        filtered = new List<Product>();

        foreach (Product p in products)
            if (p.GetPrice() >= min && p.GetPrice() <= max)
                filtered.Add(p);
        return filtered;
    }


    public List<Product> FindByDateBefore(DateTime date)
    {
        List<Product> filtered = null;
        // Handle empty list
        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");
        filtered = new List<Product>();
        foreach (Product p in products)
            if (p.CreatedAt < date)
                filtered.Add(p);
        return filtered;
    }

    public List<Product> FindByNameLike(string name)
    {
        // Handle empty list

        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");

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

    public List<Product> FindByManufacturerNameLike(string manufacturerName)
    {
        // Handle empty list

        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, can't find element.");

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

    public bool Save(Product product, Manufacturer manufacturer)
    {
        if(product is null || manufacturer is null)
            throw new InvalidOperationException("Product or manufacturer are null.");
        if (AlreadyExists(product))
            throw new DuplicateWaitObjectException("Duplicate Product. Aborting operation.");
        if (!ProductValidaitior.Validate(product, manufacturer))
            throw new InvalidOperationException("Product is not valid.");

        product.manufacturer = manufacturer;
        products.Add(product);
        return true;
    }

    public bool Update(Product product, long id)
    {
        if (!ProductValidaitior.Validate(product))
            throw new InvalidOperationException("Product is not valid.");
        if (!AlreadyExists(id))
            throw new ArgumentNullException("Product Doesn't exist yet, can't be updated.");
        products.ForEach(p =>
        {
            if (p.GetId() == id)
            {
                p.Name = product.Name;
                p.SetPrice(product.GetPrice());
            }
        });
        return true;
    }

    public bool Delete(long id)
    {
        return AlreadyExists(id) && products.Remove(FindById(id));
    }

    public bool DeleteAll()
    {
        products.Clear();
        return Count() == 0;
    }

    // ----- utilities ----- //

    public bool AlreadyExists(Product product)
    {
        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, product can't be duplicate.");

        foreach (Product p in products)
        {
            if (p.Equals(product))
                return true;
        }
        return false;
    }

    public bool AlreadyExists(long id)
    {
        Product productTocheck = FindById(id);

        if (Count() == 0)
            throw new InvalidOperationException("Lists is empty, product can't be duplicate.");

        foreach (Product p in products)
            if (p.Equals(productTocheck))
                return true;
        return false;
    }

    public double SumAllPrices()
    {
        if (Count() == 0)
            throw new InvalidOperationException("Products list Empty");

        double sum = 0;
        foreach (Product product in products)
            sum += product.GetPrice();
        return sum;
    }

    public double SumGrossBenefit()
    {
        if (Count() == 0)
            throw new InvalidOperationException("Products list Empty");
        double sum = 0;
        foreach (Product product in products)
            sum += product.GetPrice() * product.Stock;
        return Math.Round(sum, 2);
    }

    public double SumNetBenefit()
    {
        if (Count() == 0)
            throw new InvalidOperationException("Products list Empty");
        double sum = 0;
        foreach (Product product in products)
            sum += (product.GetPrice() - product.Cost) * product.Stock;
        return Math.Round(sum, 2);
    }

    public List<Product> ProductsIva(double iva = 21)
    {
        if (Count() == 0)
            throw new InvalidOperationException("Products list Empty");
        if (iva <= 0 || iva > 100)
            throw new InvalidOperationException("Iva is out of range");
        List<Product> productsPlusIva = new List<Product>();
        foreach (Product product in products) 
        {
            Product clone = product.CreateDeepCopy(products);
            clone.SetPrice(clone.GetPrice() * (iva * 0.01 + 1));
            productsPlusIva.Add(clone);
        }    
        return productsPlusIva;
    }

    // Count products list
    public long Count()
    {
        return products.Count();
    }
    public string PrintAllProducts()
    {
        if (Count() == 0)
            throw new InvalidOperationException("List empty. Can't print list");
        return String.Join(" ", products);
    }

    public string PrintList(List<Product> list)
    {
        if (Count() == 0 || list.Count() == 0)
            throw new InvalidOperationException("0 Elements. Lists is empty.");
        return "Printing List: \n" + String.Join(" ", list);
    }
}
