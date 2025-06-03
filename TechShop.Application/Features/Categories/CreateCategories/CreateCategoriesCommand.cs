using MediatR;
using TechShop.Domain.DTOs.Categories;

namespace TechShop.Application.Features.Categories.CreateCategories
{
    public record CreateCategoriesCommand(CreateCategoriesDto Dto) : IRequest<CategoriesDto>;
    
}
