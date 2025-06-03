using MediatR;
using TechShop.Domain.DTOs.Categories;

namespace TechShop.Application.Features.Categories.GetCategoriesById
{
    public record GetCategoriesByIdQuery(int id) : IRequest<CategoriesDto?>;
}
