using MediatR;

namespace TechShop.Application.Features.Categories.UpdateCategories
{
    public record UpdateCategoriesCommand(
        int id,
        string Name,
        string Description
        ) : IRequest<bool>;
}
