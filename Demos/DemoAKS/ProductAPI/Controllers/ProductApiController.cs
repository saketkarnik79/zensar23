using ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductApiController:ControllerBase
{
    private static List<Product> products = new List<Product>()
    {
        new Product() { Id = 1, Name = "Laptop", Price = 85000},
        new Product() { Id = 2, Name = "Mouse", Price = 1500},
        new Product() { Id = 3, Name = "Keyboard", Price = 2500}
    };

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = products.SingleOrDefault(p => p.Id == id);
        if(product != null)
        {
            return Ok(product);
        }
        else
        {
            return NotFound($"Product with id: {id} is not available.");
        }
    }
}