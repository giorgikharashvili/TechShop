using MediatR;
using TechShop.Domain.DTOs.Categories;

namespace TechShop.Application.Features.Categories.CreateCategories
{
    public record CreateCategoriesCommand(
        int id,
        string Name,
        string Description
        ) : IRequest<CategoriesDto>;
    
}
