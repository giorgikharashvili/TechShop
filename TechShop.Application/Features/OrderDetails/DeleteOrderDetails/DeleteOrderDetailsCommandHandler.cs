using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteOrderDetails
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteOrderDetailsCommand, bool>
    {
        private readonly IRepository<OrderDetails> _repository;

        public DeleteCartCommandHandler(IRepository<OrderDetails> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
