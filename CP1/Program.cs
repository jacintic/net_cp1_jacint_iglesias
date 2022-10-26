
using CP1.Repositories;
using CP1.Models;
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

Console.WriteLine("\n\n===== FindByManufacturerNameLike('msi') =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike("msi")));


// 2. Menú de opciones interactivo que se repita todo el tiempo

// Gestionar excepciones si ocurren

// Si se sele


// Utilities

List<Product> GenerateProductList()
{
    return new List<Product>
        {
            new Product { Name = "MacBook Pro", Price = 2500.35, manufacturer = mac},
            new Product { Name = "MSI Modern", Price = 1700.85, manufacturer = msi},
            new Product { Name = "Asus A55A", Price = 950.75, manufacturer = asus},
            new Product { Name = "Dell Pro 300", Price = 1230.65, manufacturer = dell},
            new Product { Name = "MSI Gaming Pro", Price = 1850.85, manufacturer = msi},
            new Product { Name = "Asus 305D", Price = 850, manufacturer = asus},
        };
}