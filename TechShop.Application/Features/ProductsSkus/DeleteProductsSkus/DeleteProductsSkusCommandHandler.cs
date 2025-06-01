using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteProductsSkus
{
    public class DeleteProductsSkusCommandHandler : IRequestHandler<DeleteProductsSkusCommand, bool>
    {
        private readonly IRepository<ProductsSkus> _repository;

        public DeleteProductsSkusCommandHandler(IRepository<ProductsSkus> repository)
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
