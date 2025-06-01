using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteCartItem
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartItemCommand, bool>
    {
        private readonly IRepository<CartItem> _repository;

        public DeleteCartCommandHandler(IRepository<CartItem> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
