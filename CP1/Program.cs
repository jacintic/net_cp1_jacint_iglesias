
using CP1.Repositories;
using CP1.Models;
using System.Diagnostics;
// 1. Crear objetos repositorio

// instance repo
IProductRepository productListRepository = new ProductListRepository();

// generate a few Manufacturers
Manufacturer mac = new Manufacturer { Name = "Macintosh" };
Manufacturer asus = new Manufacturer { Name = "Asus" };
Manufacturer dell = new Manufacturer { Name = "Dell" };
Manufacturer msi = new Manufacturer { Name = "MSI" };

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
Product product1 = new Product { Name = "Asus XF0RCE"};
product1.SetPrice(1635.75);
bool result = productListRepository.Save(product1, asus);
Console.Write(result ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike("asus")));
// fail - duplicate
Console.WriteLine("\n===== Save(duplicate product) =====");
Product product2 = new Product { Name = "MacBook Pro" };
product1.SetPrice(2500.35);
bool result2 = productListRepository.Save(product2, mac);
Console.Write(result2 ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike("mac")));
// fail - invalid product (name too short)
Console.WriteLine("\n===== Save(invalid product) (name too short 'ab') =====");
Product product3 = new Product { Name = "ab" };
product3.SetPrice(2500.35);
bool result3 = productListRepository.Save(product3, mac);
Console.Write(result3 ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByNameLike("ab")));


// update Product from products list
Console.WriteLine("\n\n===== Update product(product, id) =====");
Product mac2 = new Product { Name = "Macbook Air Pro X-Gaming" };
mac2.SetPrice(2750.45);
bool resUpdate = productListRepository.Update(mac2,1);
Console.Write(resUpdate ? "Product Update Successfully" : "ERROR: Product couldn't be Updated");
Console.WriteLine(productListRepository.FindById(1));

Console.WriteLine("\n===== Update product(wrong id) =====");
Product mac3 = new Product { Name = "Macbook Air Pro XL" };
mac2.SetPrice(2750.45);
bool resUpdate2 = productListRepository.Update(mac3, 730);
Console.Write(resUpdate2 ? "Product Update Successfully" : "ERROR: Product couldn't be Updated");
Console.WriteLine(productListRepository.FindById(730));


//Console.WriteLine(productListRepository.AlreadyExists(1));
//Console.WriteLine(productListRepository.FindById(1).GetId() == 1);

// 2. Menú de opciones interactivo que se repita todo el tiempo

// Gestionar excepciones si ocurren

// Si se sele


// Utilities

List<Product> GenerateProductList()
{
    Product macbook = new Product { Name = "MacBook Pro", manufacturer = mac };
    macbook.SetPrice(2500.35);
    Product msiModern = new Product { Name = "MSI Modern", manufacturer = msi };
    msiModern.SetPrice(1700.85);
    Product asusA55A = new Product { Name = "Asus A55A", manufacturer = asus };
    asusA55A.SetPrice(950.75);
    Product dellPro3000 = new Product { Name = "Dell Pro 300", manufacturer = dell };
    dellPro3000.SetPrice(1230.65);
    Product msiGamingPro = new Product { Name = "MSI Gaming Pro", manufacturer = msi };
    msiGamingPro.SetPrice(1850.85);
    Product asus305D = new Product { Name = "Asus 305D", manufacturer = asus };
    asus305D.SetPrice(850);
    return new List<Product>
        {
        /*new Product { Name = "MacBook Pro", Price = 2500.35, manufacturer = mac}.SetPrice(2500.35),
        new Product { Name = "MSI Modern", Price = 1700.85, manufacturer = msi},
        new Product { Name = "Asus A55A", Price = 950.75, manufacturer = asus},
        new Product { Name = "Dell Pro 300", Price = 1230.65, manufacturer = dell},
        new Product { Name = "MSI Gaming Pro", Price = 1850.85, manufacturer = msi},
        new Product { Name = "Asus 305D", Price = 850, manufacturer = asus},*/
            macbook, msiModern, asusA55A, dellPro3000, msiGamingPro, asus305D
        };
}