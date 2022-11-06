
using CP1.Repositories;
using CP1.Models;
using System.Diagnostics;
using System.Text;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System;
using System.Net;
using Chubrik.XConsole;
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




static ConsoleColor FromColor(Color c)
{
    int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
    index |= (c.R > 64) ? 4 : 0; // Red bit
    index |= (c.G > 64) ? 2 : 0; // Green bit
    index |= (c.B > 64) ? 1 : 0; // Blue bit

    return (System.ConsoleColor)index;
}

/*Color c = Color.FromArgb(25,0,51);
Console.BackgroundColor = FromColor(c);
Console.Clear();*/

//Console.WriteLine(text.BgColor(Color.DarkBlue));


Menu();

// Gestionar excepciones si ocurren

// Si se sele


////////////////////////////////////
// ========= Utilities ========== //
////////////////////////////////////

void Menu()
{
    string text = @"
         ███████████████████████████████████████████████████████████████████████████
         █████▄─▄▄─█▀▀▀▀▀██─▄▄▄▄█─█─█─▄▄─█▄─▄▄─███▄─▀█▀─▄█▄─▄▄─█▄─▀█▄─▄█▄─██─▄██████
         ██████─▄█▀████████▄▄▄▄─█─▄─█─██─██─▄▄▄████─█▄█─███─▄█▀██─█▄▀─███─██─███████
         █████▄▄▄▄▄████████▄▄▄▄▄█▄█▄█▄▄▄▄█▄▄▄█████▄▄▄█▄▄▄█▄▄▄▄▄█▄▄▄██▄▄██▄▄▄▄███████
         ███████████████████████████████████████████████████████████████████████████";
    string Option = "";
    do
    {
        //Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        Console.WriteLine(text.Color(224, 184, 110));
        MenuPainter();
        Option = Console.ReadLine();
        
        switch (Option)
        {
            case "1":
                int prId = 0;
                int exitCondition = 0;
                int backToMenu = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 1");
                    Console.WriteLine("Write an number from 1-6 to use the FindById(id) method");
                    try
                    {
                        Console.WriteLine("Type letter \"e\" to go back to the main menu");
                        string temp = Console.ReadLine();
                        if(temp.ToLower().Equals("e"))
                        {
                            backToMenu = 1;
                            break;
                        }
                        prId = Int32.Parse(temp);
                        if (prId <= 0 || prId > 6)
                            throw new InvalidOperationException("ERROR: input outside of the 1-6 range");
                        exitCondition = 1;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: input can't be casted to int");
                        Thread.Sleep(2000);
                    }
                    
                } while (exitCondition != 1);
                try
                {
                    if (backToMenu != 1)
                        Console.Write(productListRepository.FindById(prId));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                if (backToMenu != 1) 
                {
                    Console.WriteLine("Press any key to go back to the menu.");
                    Console.ReadLine();
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
            case "3":
                int exitCondition2 = 0;
                do
                {
                    double min = 0;
                    double max = 0;
                    Console.Clear();
                    Console.WriteLine("You chose option 3");
                    Console.WriteLine("Filter Products by minimum and maximum price.");
                    Console.WriteLine("To go back to menu write \"e\".Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        exitCondition2 = 1;
                        break;
                    }
                    int conditionForMin = 0;
                    int conditionForMax = 0;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Introduce the minimum price using \".\" as decimal point.");
                            string minS = Console.ReadLine();
                            min = Double.Parse(minS);
                            conditionForMin = 1;
                        }
                        catch (Exception)
                        { 
                            Console.WriteLine("ERROR: the input can't be converted to decimal.");
                            Thread.Sleep(2000);
                        }
                        
                    } while (conditionForMin == 0 );
                    do
                    {
                        try
                        {
                            Console.WriteLine("Introduce the maximum price using \".\" as decimal point.");
                            string maxS = Console.ReadLine();
                            max = Double.Parse(maxS);
                            
                            conditionForMax = 1;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERROR: the input can't be converted to decimal.");
                            Thread.Sleep(2000);
                        }

                    } while (conditionForMax == 0);
                    try
                    {
                        Console.Write(productListRepository.PrintList(productListRepository.FindByPriceRange(min,max)));
                        exitCondition2 = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    

                } while (exitCondition2 != 1);
                break;
            case "4":
                int exitFilterDate = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 4");
                    Console.WriteLine("Filter products before given Date.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        exitFilterDate = 1;
                        break;
                    }
                    Console.WriteLine("Introduce the date in this format: yyyy-mm-dd-hh-mm.");
                    Console.WriteLine("Hint: all products are created +60 -60 minutes from now's Date.");
                    try
                    {
                        string myDate = Console.ReadLine();
                        // parsing date
                        int year = Int32.Parse(myDate.Substring(0, 4));
                        int month = Int32.Parse(myDate.Substring(5, 2));
                        int day = Int32.Parse(myDate.Substring(8, 2));
                        int hour = Int32.Parse(myDate.Substring(11, 2));
                        int minute = Int32.Parse(myDate.Substring(14, 2));
                        //Console.WriteLine($"{year} {month} {day} {hour} {minute}");
                        //Console.ReadLine();
                        DateTime myDate2 = DateTime.Now;
                        Console.WriteLine(productListRepository.PrintList(productListRepository.FindByDateBefore(new DateTime(year,month,day,hour,minute,00))));
                        exitFilterDate = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: Wrong format.");
                        Thread.Sleep(2000);
                    }
                } while (exitFilterDate != 1);
                break;
            case "5":
                int exitFilterPName = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 6");
                    Console.WriteLine("Filter by product name LIKE %name.ToLower()%.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        exitFilterPName = 1;
                        break;
                    }
                    Console.WriteLine("Introduce the name of the product.");
                    Console.WriteLine("Hint: names like MSI, Asus, Macintosh, etc.");
                    try
                    {

                        // filter by name LIKE
                        string prodName = Console.ReadLine();

                        Console.WriteLine(productListRepository.PrintList(productListRepository.FindByNameLike(prodName)));
                        exitFilterPName = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR: Wrong format.");
                        Thread.Sleep(2000);
                    }
                } while (exitFilterPName != 1);
                break;
            case "6":
                // filter by Manufacturer Name
                int exitFilterMName = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 5");
                    Console.WriteLine("Filter by product's manufacturer name LIKE %name.ToLower()%.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    Console.WriteLine("Introduce the name of the product' smanufacturer.");
                    Console.WriteLine("Hint: names like MSI, Asus, Macintosh, etc.");
                    try
                    {

                        // filter by name LIKE
                        string prodName = Console.ReadLine();

                        Console.WriteLine(productListRepository.PrintList(productListRepository.FindByManufacturerNameLike(prodName)));
                        exitFilterMName = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitFilterMName != 1);
                break;

            case "7":
                // save product
                // example:
                // // Hoobie 3-50,50-3-100,50-Hoobie // this creates new product AND new manufacturer
                int exitSaveP = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 7");
                    Console.WriteLine("Save Product to List.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    Console.WriteLine("Introduce Parameters of the Product in the following format.");
                    Console.WriteLine("Name-Cost-Stock-Price-Manufacturer ");
                    Console.WriteLine("Hint: manufacturers like \"msi\", \"mac\" or \"asus\"");
                    Console.WriteLine("Hint: use coma \",\" to separate decimals instead of dot \".\"");
                    Console.WriteLine("Format: Name-Cost-Stock-Price-Manufacturer ");
                    try
                    {
                        // Parsing Product's parameters
                        string product = Console.ReadLine();
                        string[] productParams = product.Split('-');
                        Product productToSave = new Product 
                        { 
                            Name = productParams[0], 
                            Cost = Convert.ToDouble(productParams[1]), 
                            Stock = Convert.ToInt32(productParams[2]),
                        };
                        productToSave.SetPrice(Convert.ToDouble(productParams[3]));
                        // getting manufacturer
                        Manufacturer mf1 = null;
                        try 
                        {
                            mf1 = GenerateManufacturers()[productParams[4].ToLower()];
                        }
                        catch 
                        {
                            // manufacturer doesn't exist, do nothing but prevent from exiting main try catch block so 
                            // transaction will continue
                            // warning will be shown next iteration
                        }
                        // checking if it doesn't exist to create it
                        if (mf1 is null)
                        {
                            mf1 = new Manufacturer { Name = productParams[4]};
                            Console.WriteLine("Manufacturer doesn't exist. Creating new Manufacturer: " + (manufacturerRepository.Save(mf1) ? "Manufacturer saved successfully" : "Error while saving new Manufacturer"));
                        }

                        productListRepository.Save(productToSave, mf1);
                        Console.WriteLine("Printing Saved Product:");
                        Console.WriteLine(productListRepository.FindById(productToSave.GetId()));
                        exitSaveP = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitSaveP != 1);
                break;
            case "8":
                // update product
                // example:
                // // Name-Price-Id
                // // Hoobie 3-1500,35-3 // this updates product Id 3 and changes name and price
                int exitUpdate = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 8");
                    Console.WriteLine("Update Product from List.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    Console.WriteLine("Introduce Parameters of the Product (Name, Price and Id) in the following format.");
                    Console.WriteLine("Name-Price-Id");
                    Console.WriteLine("Hint: use coma \",\" to separate decimals instead of dot \".\"");
                    Console.WriteLine("Format: Name-Price-Id ");
                    try
                    {
                        // Parsing Product's parameters
                        string product = Console.ReadLine();
                        string[] productParams = product.Split('-');
                        Product productToUpdate = new Product
                        {
                            Name = productParams[0]
                        };
                        productToUpdate.SetPrice(Convert.ToDouble(productParams[1]));
                        long id = long.Parse(productParams[2]);
                        productListRepository.Update(productToUpdate, id);
                        Console.WriteLine("Printing Updated Product:");
                        Console.WriteLine(productListRepository.FindById(id));
                        exitUpdate = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitUpdate != 1);
                break;
            case "9":
                // Delete By Id
                // example:
                // // 1
                int exitDeleteId = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 9");
                    Console.WriteLine("Delete Product from List.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    Console.WriteLine("Introduce The Id of the Product");
                    Console.WriteLine("Hint: safe Ids range from 1-6");
                    long id = 0;
                    try
                    {
                        // Parsing Product's parameters
                        try
                        {
                            id = long.Parse(Console.ReadLine());

                        }
                        catch
                        {
                            Console.WriteLine("ERROR: Id was inserted in wrong format");
                            Thread.Sleep(2000);
                            break;
                        }

                        Console.WriteLine(productListRepository.Delete(id) ? "Product deleted successfully" : "Error while deleting Product");
                        Console.WriteLine("Checking product's Id on repo List:");
                        try
                        {
                            Console.WriteLine(productListRepository.FindById(id));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Thread.Sleep(3500);
                        }

                        exitDeleteId = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitDeleteId != 1);
                break;
            case "10":
                // Delete By All Products
                int exitDeleteAll = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 10");
                    Console.WriteLine("Delete All Products from List.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    try
                    {
                        
                        Console.WriteLine(productListRepository.DeleteAll() ? "Products deleted successfully" : "Error while deleting Products");
                        Console.WriteLine("Checking product's on repo List:");
                        try
                        {
                            Console.WriteLine(productListRepository.PrintAllProducts());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Thread.Sleep(3500);
                        }

                        exitDeleteAll = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitDeleteAll != 1);
                break;
            case "11":
                // Sum All Products Prices
                int exitSumAllProds = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 11");
                    Console.WriteLine("Sum all Products.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    try
                    {

                        Console.WriteLine("Total Prices added: " + productListRepository.SumAllPrices() + "€");
                        exitSumAllProds = 1;
                        try
                        {
                            Console.WriteLine("Press any key to Print All Products Now. Press \"s\" to skip this step.");
                            string skip = Console.ReadLine();
                            if (skip.ToLower().Equals("s"))
                            {
                                break;
                            }
                            Console.WriteLine(productListRepository.PrintAllProducts());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Thread.Sleep(3500);
                        }

                        exitDeleteAll = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitSumAllProds != 1);
                break;
            case "12":
                // Sum All Products Gross Prices
                int exitSumGrossAllProds = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 12");
                    Console.WriteLine("Gross Sum of all Products.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    try
                    {

                        Console.WriteLine("Total Gross Prices added: " + productListRepository.SumGrossBenefit() + "€");
                        exitSumGrossAllProds = 1;
                        try
                        {
                            Console.WriteLine("Press any key to Print All Products Now. Press \"s\" to skip this step.");
                            string skip = Console.ReadLine();
                            if (skip.ToLower().Equals("s"))
                            {
                                break;
                            }
                            Console.WriteLine(productListRepository.PrintAllProducts());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Thread.Sleep(3500);
                        }

                        exitSumGrossAllProds = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitSumGrossAllProds != 1);
                break;
            case "13":
                // Sum All Products Net Prices
                int exitSumNetAllProds = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 13");
                    Console.WriteLine("Net Sum of all Products.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    try
                    {

                        Console.WriteLine("Total Gross Prices added: " + productListRepository.SumNetBenefit() + "€");
                        exitSumNetAllProds = 1;
                        try
                        {
                            Console.WriteLine("Press any key to Print All Products Now. Press \"s\" to skip this step.");
                            string skip = Console.ReadLine();
                            if (skip.ToLower().Equals("s"))
                            {
                                break;
                            }
                            Console.WriteLine(productListRepository.PrintAllProducts());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Thread.Sleep(3500);
                        }

                        exitSumGrossAllProds = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitSumNetAllProds != 1);
                break;
            case "14":
                // Sum All Products Net Prices
                int exitIvaProds = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("You chose option 14");
                    Console.WriteLine("Products with their IVA shown and Passed by parameter.");
                    Console.WriteLine("To go back to menu write \"e\". Else press any other key.");
                    string temp = Console.ReadLine();
                    if (temp.ToLower().Equals("e"))
                    {
                        break;
                    }
                    try
                    {
                        int iva = 21;
                        Console.WriteLine("Insert IVA (between 1-100). To skip this step press \"s\".");
                        string ivaS = Console.ReadLine();
                        Console.WriteLine("Printing all Products With IVA");
                        if (!ivaS.ToLower().Equals("s"))
                        {
                            try
                            {
                                iva = int.Parse(ivaS);
                                Console.WriteLine(productListRepository.PrintList(productListRepository.ProductsIva(iva)));

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else 
                        {
                            try
                            {
                                Console.WriteLine(productListRepository.PrintList(productListRepository.ProductsIva()));
                                exitIvaProds = 1;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Thread.Sleep(3500);
                            }
                        }
                        Console.WriteLine("Printing all Products With IVA");


                        exitIvaProds = 1;
                        Console.WriteLine("Press any key to go back to the menu.");
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(2000);
                    }
                } while (exitIvaProds != 1);
                break;
            default:
                break;
        }}
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


//////////////////////////////////////
/////////// MENU PAINTER /////////////
//////////////////////////////////////


void MenuPainter()
{
    int tableWidth = 75;
    PrintLine(tableWidth);
    PrintRow(tableWidth, "Write the number of the option you want to select.");
    PrintLine(tableWidth);
    PrintRowII(tableWidth,"Option", "Action");
    PrintLine(tableWidth);
    PrintRowII(tableWidth,"1", "Print Product by Id");
    PrintLine(tableWidth);
    PrintRowII(tableWidth,"2", "Print All Products");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "3", "Filter Products by minimum and maximum price");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "4", "Filter Products before given date");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "5", "Filter Products by name of the product");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "6", "Filter Products by name of the product's manufacturer");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "7", "Save a Product in the List");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "8", "Update a Product in the List");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "9", "Delete Product by Id");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "10", "Delete All Products");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "11", "Sum all Product prices");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "12", "Sum all Gross benefit from Products");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "13", "Sum Net benefit from Products");
    PrintLine(tableWidth);
    PrintRowII(tableWidth, "14", "Show Product Prices + IVA");
    PrintLine(tableWidth);
    Console.WriteLine("         Write \"exit\" to exit".Color(255, 242, 0));
}


static void PrintLine(int tableWidth)
{
    string space = new string(' ', 9);
    Console.WriteLine(space + new string('═', tableWidth).Color(27, 12, 82));
}

static void PrintRow(int tableWidth, params string[] columns)
{
    string space = new string(' ', 9);
    int width = (tableWidth - columns.Length) / columns.Length;
    string row = space + "║".Color(27, 12, 82);

    foreach (string column in columns)
    {
        row += AlignCentre(column, width - 1).Color(112, 174, 212) + "║".Color(27, 12, 82);
    }

    Console.WriteLine(row);
}

static void PrintRowII(int tableWidth, params string[] columns)
{
    string space = new string(' ', 9);
    int firstRow = 8;
    int width = (tableWidth - columns.Length) / columns.Length;
    string row = "║";


    row += AlignCentre(columns[0], firstRow) + "║";
    row += AlignCentre(columns[1], tableWidth - firstRow - 3) + "║";

    string[] myRows = row.Split("║");
    for (int i = 0; i < myRows.Length; i++)
    {
        if (i == 0)
        {
            Console.Write(space + "║".Color(27, 12, 82));
        }
        else if (i == 1) {
            Console.Write(myRows[i].Color(0, 229, 255));
            Console.Write("║".Color(27, 12, 82));
        }
        else if (i == 2) {
            Console.Write(myRows[i]);
            Console.Write("║".Color(27, 12, 82));
        }
    }
    Console.WriteLine();
}
static string AlignCentre(string text, int width)
{
    text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

    if (string.IsNullOrEmpty(text))
    {
        return new string(' ', width);
    }
    else
    {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}