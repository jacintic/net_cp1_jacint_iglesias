using CP1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO1;

public class ProductValidaitior
{
    public bool Validate(Product product, Manufacturer manufacturer)
    {
        return
            ValidateName(product.Name) &&
            //ValidateWeight(product.Weight) &&
            //ValidatePriceAndCost(product.GetPrice(), product.Cost) &&
            ValidateCreatedAt(product.CreatedAt) &&
            ValidateManufacturer(manufacturer);
    }
    public bool Validate(Product product)
    {
        return
            ValidateName(product.Name) &&
            //ValidateWeight(product.Weight) &&
            //ValidatePriceAndCost(product.GetPrice(), product.Cost) &&
            ValidateCreatedAt(product.CreatedAt);
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
    // // -- end of validate sub- methods
}
