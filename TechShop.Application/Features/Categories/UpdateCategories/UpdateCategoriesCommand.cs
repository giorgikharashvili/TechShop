using MediatR;
using TechShop.Domain.DTOs.Categories;

namespace TechShop.Application.Features.Categories.UpdateCategories
{
    public record UpdateCategoriesCommand(int id, UpdateCategoriesDto Dto) : IRequest<bool>;
}
