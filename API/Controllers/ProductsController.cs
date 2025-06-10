using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type,string? sort)
    {
        return Ok(await productRepository.GetProductsAsync(brand,type, sort));
    }

    [HttpGet("{id:int}")] // aip/products/12323
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        productRepository.AddProduct(product);

        if (await productRepository.SaveChangesAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id });
        }
      
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product) {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("cannot find specified Product Id");

        productRepository.UpdateProduct(product);
        if (await productRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Product cannot be updated");

    }

    [HttpDelete("{id:int}")]

    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await productRepository.GetProductByIdAsync(id);
        

        if (product == null) return NotFound();

        productRepository.DeleteProduct(product);

       if (await productRepository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Product cannot be deleted");

    }

    [HttpGet("brands")]
    public async Task<ActionResult<string>> GetBrands() {
        return Ok(await productRepository.GetBrandAsync());
}
 [HttpGet("types")]
    public async Task<ActionResult<string>> GetTypes() {
        return Ok(await productRepository.GetTypeAsync());
}
    public bool ProductExists(int id)
    {
        return productRepository.ProductExists(id);
    }
}
