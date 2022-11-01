using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP1.Models;

namespace CP1.Repositories; 
public interface IManufacturerRepository {

    // declara métodos para trabajar con objetos Manufacturer
    public List<Manufacturer> Manufacturers
    {
        get;
        set;
    }
    // Buscar por id
    Manufacturer FindById(long id);

    // Buscar todos
    List<Manufacturer> FindAll();

    // Guardar nuevo fabricante, replicar lo mismo para generar el id de fabricante que se hizo en productos.
    bool Save(Manufacturer manufacturer);

    // Actualizar fabricante
    bool Update(Manufacturer manufacturer);

    // Borrar fabricante por id
}


/**
 * Crear una **clase** que implemente la interfaz 
 * creada y proporcione el cuerpo de cada método 
 * declarado en la interfaz. La clase tendrá un 
 * atributo lista de productos sobre la que 
 * realizará las operaciones implementadas.

4. **Interfaces** y métodos para **Manufacturer**
Siguiendo el punto anterior, crear una interfaz y una 
clase para realizar operaciones con fabricantes 
(Manufacturer). Esta vez con menos operaciones, 
solamente las básicas:
* Buscar por id
* Buscar todos
* Guardar nuevo fabricante, replicar lo mismo para generar el id de fabricante que se hizo en productos.
* Actualizar fabricante
* Borrar fabricante por id
 */