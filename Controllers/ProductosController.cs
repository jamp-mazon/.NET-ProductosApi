using Microsoft.AspNetCore.Mvc;
using ProductosApi.Models;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductosController : ControllerBase
{
    //Definimos la lista de productos cada producto esta dentro de un array
    private static readonly List<Producto> productos = new() //Este endpoint puede devolver una lista de productos junto con una respuesta HTTP.
    {
        new Producto
        {
            Id=1,
            Nombre= "Teclado",
            Precio = 19.99m ,
            Stock = 10
        },
        new Producto
        {
            Id = 2,
            Nombre= "Ratón",
            Precio = 12.50m , 
            Stock = 5
        },
        new Producto
        {
            Id = 3,
            Nombre= "Monitor",
            Precio= 149.99m ,
            Stock = 3
        }
    };
    //Nuestro get que dara de resultado un 200 tambien devolvera la lista de productos GET api/productos por Route... 
    [HttpGet]
    public ActionResult<List<Producto>> GetProductos()
    {
        return Ok(productos);
    }
    [HttpGet("{id}")]
    public ActionResult<Producto> GetProducto(int id)
    {
        Producto? producto = productos.FirstOrDefault(p => p.Id == id); //operacion lambda devuelveme producto que sea un ID igual al que entra 
        //Producto? puede ser null.
        if (producto==null)//Si no lo encuentra devuelve null
        {
            return NotFound();//Con lo cual no funciona si el producto es null
        }
        return Ok(producto); //Si todo va bien me devuelve el producto
    }

    [HttpPost]
    public ActionResult<Producto> CrearProducto (Producto nuevoProducto)
    {
        nuevoProducto.Id = productos.Max(p => p.Id) + 1; //El nuevo producto sera el ID mayor +1 y asi nos aseguramos que sean consecutivos y no se repitan
        productos.Add(nuevoProducto); //Añadimos el nuevo producto a la lista que tenemos creada de productos.

        return CreatedAtAction(
            nameof (GetProducto),
            new {id = nuevoProducto.Id},
            nuevoProducto
            );
    }
}