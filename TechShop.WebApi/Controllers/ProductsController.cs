﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Application.Features.Products.GetAllProducts;
using TechShop.Application.Features.Products.CreateProducts;
using TechShop.Application.Features.Products.DeleteProducts;
using TechShop.Application.Features.Products.GetProductsById;
using TechShop.Application.Features.Products.UpdateProducts;
using Microsoft.AspNetCore.Authorization;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductsDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
        public async Task<ActionResult<ProductsDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductsByIdQuery(id));
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
        public async Task<ActionResult<ProductsDto>> Create([FromBody] CreateProductsCommand command)
        {
            var created = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsCommand command)
        {
            if (id != command.id) return BadRequest("ID mismatch");

            var isSuccess = await _mediator.Send(command);
            if (!isSuccess) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _mediator.Send(new DeleteProductsCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}