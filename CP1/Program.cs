
using CP1.Repositories;
using CP1.Models;
using System.Diagnostics;
using System.Text;
// 0. set console so it can print €
Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Crear objetos repositorio

// instance repo
IProductRepository productListRepository = new ProductListRepository();

// generate basic Product List
List<Product> products = GenerateProductList();



// fill repo with default product list
productListRepository.Products = products;

// find by id
Console.WriteLine("===== FindById(2) =====");
Console.WriteLine(productListRepository.FindById(2));

// find all products
Console.WriteLine("\n\n===== FindAll() =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindAll()));

// filter by price range
Console.WriteLine("\n\n===== FindByPriceRange(1000,3000) =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByPriceRange(1000,3000)));

// filter by param date is before
Console.WriteLine("\n\n===== FindByDateBefore(DateTime.Now) =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByDateBefore(DateTime.Now)));

// filter by name LIKE
Console.WriteLine("\n\n===== FindByNameLike('asus') =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByNameLike("asus")));
Console.WriteLine("\n===== FindByNameLike('non existing name') =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByNameLike("non existing name")));

// filter by Manufacturer Name
Console.WriteLine("\n\n===== FindByManufacturerNameLike('msi') =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike("msi")));


// save Product in products list
Console.WriteLine("\n\n===== Save(product, manufacturer) =====");
Product product1 = new Product { Name = "Asus XF0RCE", Cost = 950.35, Stock = 3};
product1.SetPrice(1635.75);
bool result = productListRepository.Save(product1, GenerateManufacturers()["asus"]);
Console.WriteLine(result ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike("asus")));
// fail - duplicate
Console.WriteLine("\n===== Save(duplicate product) =====");
Product product2 = new Product { Name = "MacBook Pro", Cost = 1250.55, Stock = 2 };
product1.SetPrice(2500.35);
bool result2 = productListRepository.Save(product2, GenerateManufacturers()["mac"]);
Console.WriteLine(result2 ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike("mac")));
// fail - invalid product (name too short)
Console.WriteLine("\n===== Save(invalid product) (name too short 'ab') =====");
Product product3 = new Product { Name = "ab"};
product3.SetPrice(2500.35);
bool result3 = productListRepository.Save(product3, GenerateManufacturers()["mac"]);
Console.WriteLine(result3 ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByNameLike("ab")));


// update Product from products list
Console.WriteLine("\n\n===== Update product(product, id) =====");
Product mac2 = new Product { Name = "Macbook Air Pro X-Gaming" };
mac2.SetPrice(2750.45);
bool resUpdate = productListRepository.Update(mac2,1);
Console.WriteLine(resUpdate ? "Product Update Successfully" : "ERROR: Product couldn't be Updated");
Console.WriteLine(productListRepository.FindById(1));

Console.WriteLine("\n===== Update product(wrong id) =====");
Product mac3 = new Product { Name = "Macbook Air Pro XL" };
mac2.SetPrice(2750.45);
bool resUpdate2 = productListRepository.Update(mac3, 730);
Console.WriteLine(resUpdate2 ? "Product Update Successfully" : "ERROR: Product couldn't be Updated");
Console.WriteLine(productListRepository.FindById(730));


// Delete a single product by Id, success and fail
Console.WriteLine("\n\n===== Delete product(id) =====");
Console.WriteLine(productListRepository.Delete(7) ? "Product Deleted Successfully" : "ERROR: Product couldn't be Deleted");
Console.WriteLine("Trying to fetch deleted prodcut with ID 7");
Console.WriteLine(productListRepository.FindById(7));
// fail
Console.WriteLine("\n\n===== Delete product(id) (Fail) =====");
Console.WriteLine(productListRepository.Delete(567567) ? "Product Deleted Successfully" : "ERROR: Product couldn't be Deleted");
Console.WriteLine("Trying to fetch deleted prodcut with ID 567567");
Console.WriteLine(productListRepository.FindById(7567567));

// Sum All
Console.WriteLine("\n\n===== Sum All Prices =====");

Console.WriteLine(productListRepository.SumAllPrices() + "€");
Console.WriteLine("Printing all products");
Console.WriteLine(productListRepository.PrintAllProducts());


// Sum All Gross benefit taking stock into account
Console.WriteLine("\n\n===== Sum All Prices Gross (stock) =====");
Console.WriteLine(productListRepository.SumGrossBenefit() + "€");

// Sum All Gross benefit taking stock and cost into account
Console.WriteLine("\n\n===== Sum All Prices Net (stock,cost) =====");
Console.WriteLine(productListRepository.SumNetBenefit() + "€");


// Sum All Gross benefit taking stock, IVA and cost into account
Console.WriteLine("\n\n===== Sum All Prices Net (stock,cost, IVA) -- IVA default, 21% =====");
Console.WriteLine(productListRepository.SumNetBenefitIva() + "€");

Console.WriteLine("\n===== Sum All Prices Net (stock,cost, IVA) -- IVA 31% =====");
Console.WriteLine(productListRepository.SumNetBenefitIva(31) + "€");

Console.WriteLine("\n===== Sum All Prices Net (stock,cost, IVA) -- Wrong IVA 101% =====");
try
{
    Console.WriteLine(productListRepository.SumNetBenefitIva(101) + "€");
} 
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


Console.WriteLine("\n\n\n" + productListRepository.PrintList(productListRepository.FindAll()));
Console.WriteLine("\n===== Diferent Id constructor methods - notice 1 with static and another one with Max Id =====");
Dictionary<string, Manufacturer> manufacturers = GenerateManufacturers();
Product testProduct = new Product(productListRepository.FindAll()) { Name = "Testo 1X460", manufacturer = manufacturers["asus"], Cost = 350.35, Stock = 2 };
testProduct.SetPrice(850);
Console.WriteLine("Id generated by getting Max Id from ListRepo. Id: " + testProduct.GetId());
Product testProductII = new Product { Name = "TestoII 1X461", manufacturer = manufacturers["asus"], Cost = 350.35, Stock = 2 };
testProductII.SetPrice(850);
Console.WriteLine("Id generated by getting static NextProductId. Id: " + testProductII.GetId());


// Delete All
Console.WriteLine("\n\n===== Delete All =====");
Console.WriteLine(productListRepository.DeleteAll() ? "Products Deleted Successfully" : "ERROR: Products couldn't be Deleted");
Console.WriteLine("Printing all products");
Console.WriteLine(productListRepository.PrintAllProducts());



// 2. Menú de opciones interactivo que se repita todo el tiempo

// Gestionar excepciones si ocurren

// Si se sele


// Utilities

Dictionary<string, Manufacturer> GenerateManufacturers() 
{
    // generate a few Manufacturers
    return new Dictionary<string, Manufacturer>() 
    {
        {"mac", new Manufacturer { Name = "Macintosh" } },
        {"asus", new Manufacturer { Name = "Asus" } },
        {"dell", new Manufacturer { Name = "Dell" } },
        {"msi", new Manufacturer { Name = "MSI" } },
    };
};

List<Product> GenerateProductList()
{
    Dictionary<string, Manufacturer> manufacturers = GenerateManufacturers();
    Product macbook = new Product { Name = "MacBook Pro", manufacturer = manufacturers["mac"], Cost = 1350.55, Stock = 3 };
    macbook.SetPrice(2500.35);
    Product msiModern = new Product { Name = "MSI Modern", manufacturer = manufacturers["msi"], Cost = 995.35, Stock = 2 };
    msiModern.SetPrice(1700.85);
    Product asusA55A = new Product { Name = "Asus A55A", manufacturer = manufacturers["asus"], Cost = 550.35, Stock = 4 };
    asusA55A.SetPrice(950.75);
    Product dellPro3000 = new Product { Name = "Dell Pro 300", manufacturer = manufacturers["dell"], Cost = 950.35 , Stock = 5 };
    dellPro3000.SetPrice(1230.65);
    Product msiGamingPro = new Product { Name = "MSI Gaming Pro", manufacturer = manufacturers["msi"], Cost = 1150.35 , Stock = 1 };
    msiGamingPro.SetPrice(1850.85);
    Product asus305D = new Product { Name = "Asus 305D", manufacturer = manufacturers["asus"], Cost = 350.35 , Stock = 2 };
    asus305D.SetPrice(850);
    return new List<Product>
    {
        macbook, msiModern, asusA55A, dellPro3000, msiGamingPro, asus305D
    };
}

