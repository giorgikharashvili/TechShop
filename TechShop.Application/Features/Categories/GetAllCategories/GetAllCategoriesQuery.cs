using MediatR;
using TechShop.Domain.DTOs.Categories;

namespace TechShop.Application.Features.Categories.GetAllCategories
{
    public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoriesDto>>;
  
}
