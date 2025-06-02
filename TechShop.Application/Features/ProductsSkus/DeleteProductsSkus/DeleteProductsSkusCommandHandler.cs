using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;


namespace TechShop.Application.Features.ProductsSkus.DeleteProductsSkus
{
    public class DeleteProductsSkusCommandHandler : IRequestHandler<DeleteProductsSkusCommand, bool>
    {
        private readonly IRepository<Domain.Entities.ProductsSkus> _repository;

        public DeleteProductsSkusCommandHandler(IRepository<Domain.Entities.ProductsSkus> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductsSkusCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
