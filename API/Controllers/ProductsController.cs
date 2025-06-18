using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> productRepository) : ControllerBase
{
    

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type,string? sort)
    {

        var spec = new ProductSpecification(brand, type,sort);
        var products = await productRepository.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("{id:int}")] // aip/products/12323
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        productRepository.Add(product);

        if (await productRepository.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id });
        }
      
        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product) {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("cannot find specified Product Id");

        productRepository.Update(product);
        if (await productRepository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Product cannot be updated");

    }

    [HttpDelete("{id:int}")]

    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        

        if (product == null) return NotFound();

        productRepository.Remove(product);

       if (await productRepository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Product cannot be deleted");

    }

    [HttpGet("brands")]
    public async Task<ActionResult<string>> GetBrands() {

        var spec = new BrandListSpecification();
        return Ok(await productRepository.ListAsync(spec));
}
 [HttpGet("types")]
    public async Task<ActionResult<string>> GetTypes() {
        var spec = new TypeListSpecification();
        return Ok(await productRepository.ListAsync(spec));
}
    public bool ProductExists(int id)
    {
        return productRepository.Exists(id);
    }
}
