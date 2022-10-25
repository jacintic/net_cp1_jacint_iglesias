
using CP1.Repositories;
using CP1.Models;
// 1. Crear objetos repositorio

// instance repo
IProductRepository productListRepository = new ProductListRepository();

// instance basic Product List
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


// 2. Menú de opciones interactivo que se repita todo el tiempo

// Gestionar excepciones si ocurren

// Si se sele


// Utilities

List<Product> GenerateProductList()
{
    return new List<Product>
        {
            new Product { Name = "MacBook Pro", Price = 2500.35 },
            new Product { Name = "MSI Modern", Price = 1700.85 },
            new Product { Name = "Asus A55A", Price = 950.75 },
            new Product { Name = "Dell Pro 300", Price = 1230.65 },
            new Product { Name = "MSI Gaming Pro", Price = 1850.85 },
            new Product { Name = "Asus 305D", Price = 850 },
        };
}