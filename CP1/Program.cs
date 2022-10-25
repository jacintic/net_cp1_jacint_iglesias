// 1. Crear objetos repositorio
using CP1.Repositories;

IProductRepository productRepository = new ProductListRepository();

Console.WriteLine(productRepository.FindById(2));



// 2. Menú de opciones interactivo que se repita todo el tiempo

// Gestionar excepciones si ocurren

// Si se sele