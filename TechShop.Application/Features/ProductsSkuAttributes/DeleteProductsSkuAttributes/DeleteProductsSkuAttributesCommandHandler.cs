using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.DeleteProductsSkuAttributes
{
    public class DeleteProductsSkuAttributesCommandHandler : IRequestHandler<DeleteProductsSkuAttributesCommand, bool>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;

        public DeleteProductsSkuAttributesCommandHandler(IRepository<ProductSkuAttributes> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteProductsSkuAttributesCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
