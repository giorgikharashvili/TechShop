using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.GetByCategoryId;

public record GetByCategoryIdQuery(int id) : IRequest<IEnumerable<ProductsDto>>;



