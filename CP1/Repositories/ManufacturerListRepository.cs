﻿using CP1.Models;
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
}
