using CP1.Models;
using POO1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP1.Repositories; 
public class ManufacturerListRepository : IManufacturerRepository{

    // implementa métodos para trabajar con objetos Manufacturer

    // Attributes
    private List<Manufacturer> manufacturers;
    //private ManufacturertValidaitior ProductValidaitior;

    // Constructor
    public ManufacturerListRepository()
    {
        manufacturers = new List<Manufacturer>();
        //ManufacturertValidaitior = new ManufacturertValidaitior();
    }

    // Getters & Setters
    public List<Manufacturer> Manufacturers
    {
        get => manufacturers;
        set => manufacturers = value;
    }

    public void SetManufacturers(List<Manufacturer> manufacturers)
    {
        this.manufacturers = manufacturers;
    }

    // methods
    public Manufacturer FindById(long id)
    {
        if (manufacturers.Count == 0)
            throw new InvalidOperationException("List is empty can't find manufacturer.");
        foreach (Manufacturer manu in manufacturers)
            if (manu.GetId() == id)
                return manu;
        throw new InvalidOperationException("Id not found on repo");
    }

    public List<Manufacturer> FindAll()
    {
        if (manufacturers.Count == 0)
            throw new InvalidOperationException("List is empty can't return list of Manufacturers.");
        return manufacturers;
    }
    public bool Save(Manufacturer manufacturer)
    {
        if (ManufacturerIsDuplicate(manufacturer))
            throw new InvalidOperationException("Manufacturer already exists (Id OR Name), Can't be Created");
        manufacturers.Add(manufacturer);
        return true;
    }

    // utilities
    public bool ManufacturerIsDuplicate(Manufacturer manufacturer)
    {
        if (manufacturers.Count == 0)
            return false;
        if (manufacturer == null || manufacturer.Name.Equals("") || manufacturer.Name == null )
            return false;
        foreach (Manufacturer manuf in manufacturers)
            if (manuf.GetId() == manufacturer.GetId() || manuf.Name.ToLower().Equals(manufacturer.Name.ToLower()))
                return true;
        return false;
    }

    public bool Update(Manufacturer manufacturer, long id)
    {
        var myManufacturer = FindById(id);
        // manufacturer => null exception already handled already by FindById method
        if (myManufacturer.Name.ToLower().Equals(manufacturer.Name.ToLower()))
            return false;
        myManufacturer.Name = manufacturer.Name;
        return true;
    }

    public bool Delete(long id)
    {
        var selectedManufacturer = FindById(id);
        return manufacturers.Remove(selectedManufacturer);
    }
}
