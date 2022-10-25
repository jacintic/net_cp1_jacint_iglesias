
using CP1.Repositories;
using CP1.Models;
// 1. Crear objetos repositorio

// instance repo
IProductRepository productListRepository = new ProductListRepository();

// instance basic Product List
List<Product> products = GenerateProductList();

// fill repo with default product list
productListRepository.Products = products;


Console.WriteLine("===== FindById(2) =====");
Console.WriteLine(productListRepository.FindById(2));



// 2. Menú de opciones interactivo que se repita todo el tiempo

// Gestionar excepciones si ocurren

// Si se sele


// Utilities

List<Product> GenerateProductList()
{
    return new List<Product>
        {
            new Product { Name = "MacBook Pro", Price = 16 },
            new Product { Name = "MSI Modern", Price = 32 },
            new Product { Name = "Asus A55A", Price = 8 },
        };
}