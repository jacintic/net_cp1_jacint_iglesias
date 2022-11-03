
using CP1.Repositories;
using CP1.Models;
using System.Diagnostics;
using System.Text;
using System.ComponentModel.Design;
using System.Data;
// 0. set console so it can print €
Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Crear objetos repositorio


////////////////////////////////////
// ======  Product Methods ====== //
////////////////////////////////////

// instance repo
IProductRepository productListRepository = new ProductListRepository();

// generate basic Product List
List<Product> products = GenerateProductList();



// fill repo with default product list
productListRepository.Products = products;



/*
// find by id
Console.WriteLine("===== FindById(2) =====");
Console.WriteLine(productListRepository.FindById(2));

// find all products
Console.WriteLine("\n\n===== FindAll() =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindAll()));

// filter by price range
Console.WriteLine("\n\n===== FindByPriceRange(1000,3000) =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByPriceRange(1000, 3000)));

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
Product product1 = new Product { Name = "Asus XF0RCE", Cost = 950.35, Stock = 3 };
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
Product product3 = new Product { Name = "ab" };
product3.SetPrice(2500.35);
bool result3 = productListRepository.Save(product3, GenerateManufacturers()["mac"]);
Console.WriteLine(result3 ? "Product Saved Successfully" : "ERROR: Product couldn't be saved");
Console.WriteLine(productListRepository.PrintList(productListRepository.FindByNameLike("ab")));


// update Product from products list
Console.WriteLine("\n\n===== Update product(product, id) =====");
Product mac2 = new Product { Name = "Macbook Air Pro X-Gaming" };
mac2.SetPrice(2750.45);
bool resUpdate = productListRepository.Update(mac2, 1);
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
Console.WriteLine("\n\n===== IVA default, 21% =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.ProductsIva()));

Console.WriteLine("\n===== IVA 31% =====");
Console.WriteLine(productListRepository.PrintList(productListRepository.ProductsIva(31)));
Console.WriteLine("\n===== IVA 101% (Fail, exception out of range) =====");
try
{
    Console.WriteLine(productListRepository.PrintList(productListRepository.ProductsIva(101)));
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.Message);
}
// // Product list check
Console.WriteLine("Print all products in repo (To Compare prices with and without IVA and show that no original Product was modified)");
Console.WriteLine(productListRepository.PrintAllProducts());


Console.WriteLine("\n\n\n" + productListRepository.PrintList(productListRepository.FindAll()));
Console.WriteLine("\n===== Diferent Id constructor methods - notice 1 with static and another one with Max Id =====");
Dictionary<string, Manufacturer> manufacturers = GenerateManufacturers();
Product testProduct = new Product(productListRepository.FindAll()) { Name = "TestI 1X460", manufacturer = manufacturers["asus"], Cost = 350.35, Stock = 2 };
testProduct.SetPrice(850);
Console.WriteLine("Id generated by getting Max Id from ListRepo. Id: " + testProduct.GetId());
Product testProductII = new Product { Name = "TestII 1X461", manufacturer = manufacturers["asus"], Cost = 350.35, Stock = 2 };
testProductII.SetPrice(850);
Console.WriteLine("Id generated by getting static NextProductId. Id: " + testProductII.GetId());


// Delete All
Console.WriteLine("\n\n===== Delete All =====");
Console.WriteLine(productListRepository.DeleteAll() ? "Products Deleted Successfully" : "ERROR: Products couldn't be Deleted");
Console.WriteLine("Printing all products");
Console.WriteLine(productListRepository.PrintAllProducts());
*/


////////////////////////////////////
// ==== Manufacturer Methods ==== //
////////////////////////////////////

// instance repo
IManufacturerRepository manufacturerRepository = new ManufacturerListRepository();

// generate basic Product List
List<Manufacturer> manufacturersList = GenerateManufacturerList();

// fill repo with default manufacturer list
manufacturerRepository.Manufacturers =  manufacturersList;

/*

Console.WriteLine("\n\n\n=========================");
Console.WriteLine("===== MANUFACTURERS =====");
Console.WriteLine("=========================");

// find by id

Console.WriteLine("\n\n===== FindById(21) (success) =====");
Console.WriteLine(manufacturerRepository.FindById(21));
Console.WriteLine("\n===== FindById(342) (fail) =====");
try
{
    Console.WriteLine(manufacturerRepository.FindById(342));
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.Message);
}

// Find All
Console.WriteLine("\n\n===== FindAll() =====");
Console.WriteLine(String.Join("\n", manufacturerRepository.FindAll()));

// Save Manufacturer
Console.WriteLine("\n\n===== Save() =====");
Console.WriteLine("1. Success");
Manufacturer manuf1 = new Manufacturer { Name = "Hewlett Packard" };
Console.WriteLine(manufacturerRepository.Save(manuf1) ? "Manufacturer Saved Successfully" : "ERROR: Manufacturer couldn't be saved");
Console.WriteLine("2. Fail");
Manufacturer manuf2 = new Manufacturer { Name = "Asus" };
try
{
    Console.WriteLine(manufacturerRepository.Save(manuf2) ? "Manufacturer Saved Successfully" : "ERROR: Manufacturer couldn't be saved");
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.Message);
}

// Update Manufacturer
Console.WriteLine("\n\n===== Update(manufacturer, id) =====");
Console.WriteLine("1. Success");
Manufacturer manuf3 = new Manufacturer { Name = "HP" };
Console.WriteLine(manufacturerRepository.Update(manuf3, 25) ? "Manufacturer Updated Successfully" : "ERROR: Manufacturer couldn't be updated");
Console.WriteLine("2. Fail (name == name)");
Manufacturer manuf4 = new Manufacturer { Name = "Macintosh" };
try
{
    Console.WriteLine(manufacturerRepository.Update(manuf4,21) ? "Manufacturer Updated Successfully" : "ERROR: Manufacturer couldn't be updated");
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.Message);
}
Console.WriteLine("3. Fail (id not found)");
Manufacturer manuf5 = new Manufacturer { Name = "Macintosh" };
try
{
    Console.WriteLine(manufacturerRepository.Update(manuf5, 35) ? "Manufacturer Updated Successfully" : "ERROR: Manufacturer couldn't be updated");
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.Message);
}

// Delete Manufacturer
Console.WriteLine("\n\n===== Delete(id) =====");
Console.WriteLine("1. Success");
Console.WriteLine(manufacturerRepository.Delete(21) ? "Manufacturer Deleted Successfully" : "ERROR: Manufacturer couldn't be deleted");
Console.WriteLine("2. Fail");
try
{
    Console.WriteLine(manufacturerRepository.Delete(21) ? "Manufacturer Deleted Successfully" : "ERROR: Manufacturer couldn't be deleted");

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

*/





// 2. Menú de opciones interactivo que se repita todo el tiempo

Menu();

// Gestionar excepciones si ocurren

// Si se sele


////////////////////////////////////
// ========= Utilities ========== //
////////////////////////////////////

void Menu()
{
    string Option = "";
    do
    {
        Console.Clear();
        Console.WriteLine("Write the number of the option you want to select.");
        Console.WriteLine("1. Print Product by Id");
        Console.WriteLine("2. Print All Products");
        Console.WriteLine("Write \"exit\" to exit");
        Option = Console.ReadLine();
        
        switch (Option)
        {
            case "1":
                int prId = 0;
                int exitCondition = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 1");
                    Console.WriteLine("Write an number from 1-6 to use the FindById(id) method");
                    try
                    {
                        prId = Int32.Parse(Console.ReadLine());
                        if (prId <= 0 || prId > 6)
                            throw new InvalidOperationException("ERROR: input outside of the 1-6 range");
                        exitCondition = 1;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2300);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: input can't be casted to int");
                        Thread.Sleep(2300);
                    }
                    
                } while (exitCondition != 1);
                try
                {
                    Console.Write(productListRepository.FindById(prId));
                    Thread.Sleep(2300);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Thread.Sleep(2300);
                }
                break;
            case "2":
                Console.Clear();
                Console.WriteLine("You chose option 2");
                Console.WriteLine("Printing List of Products now.");
                try
                {
                    Console.Write(productListRepository.PrintList(productListRepository.FindAll()));
                    Console.WriteLine("Press any key to go back to the menu.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                break;
        }
    }
    while (!Option.ToLower().Equals("exit"));
    Console.Clear();
    Console.WriteLine("You left the program, good bye!");
    Environment.Exit(1);// exit
}


// generates a Dictionary (Key, Value) to better handle instancing manufcaturers to Products
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
    Product dellPro3000 = new Product { Name = "Dell Pro 300", manufacturer = manufacturers["dell"], Cost = 950.35, Stock = 5 };
    dellPro3000.SetPrice(1230.65);
    Product msiGamingPro = new Product { Name = "MSI Gaming Pro", manufacturer = manufacturers["msi"], Cost = 1150.35, Stock = 1 };
    msiGamingPro.SetPrice(1850.85);
    Product asus305D = new Product { Name = "Asus 305D", manufacturer = manufacturers["asus"], Cost = 350.35, Stock = 2 };
    asus305D.SetPrice(850);
    return new List<Product>
    {
        macbook, msiModern, asusA55A, dellPro3000, msiGamingPro, asus305D
    };
}


List<Manufacturer> GenerateManufacturerList()
{
    Dictionary<string, Manufacturer> manufacturers = GenerateManufacturers();

    return new List<Manufacturer>
        {
            manufacturers["mac"],
            manufacturers["msi"],
            manufacturers["asus"],
            manufacturers["dell"]
        };
}

