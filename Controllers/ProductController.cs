using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PruebasAPI.Net.Models;

namespace PruebasAPI.Net.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductsContext _context;

    public ProductController(ProductsContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("crearProducto")]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        //guardar el product en la base de datos
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        //devolver un mensaje de exito
        return Ok();
    }

    [HttpGet]
    [Route("listaProducto")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        //Obten la lista de products de la base de datos
        var products = await _context.Products.ToListAsync();

        //devuelve una lista de products
        return Ok(products);
    }

    [HttpGet]
    [Route("verProducto")]
    public async Task<IActionResult> GetProduct(int id)
    {
        //obtener el product de la base de datos
        Product product = await _context.Products.FindAsync(id);

        //devolver el product
        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPut]
    [Route("editarProducto")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        //Actualizar el product en la base de datos
        var existingProduct = await _context.Products.FindAsync(id);
        existingProduct!.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;

        await _context.SaveChangesAsync();

        //devolver un mensaje de exito
        return Ok();
    }

    [HttpDelete]
    [Route("eliminarProducto")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        //Eliminar product de la base de datos
        var productRemoved = await _context.Products.FindAsync(id);
        _context.Products.Remove(productRemoved!);

        await _context.SaveChangesAsync();

        //Devolver un mensaje de exito
        return Ok();
    }
}
