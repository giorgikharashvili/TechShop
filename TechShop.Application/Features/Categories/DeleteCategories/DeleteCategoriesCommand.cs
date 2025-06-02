using MediatR;

namespace TechShop.Application.Features.Categories.DeleteCategories
{
    public record DeleteCategoriesCommand(int id) : IRequest<bool>;
}
