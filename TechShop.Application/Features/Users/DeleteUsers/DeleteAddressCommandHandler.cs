using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteAddresses
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartItemCommand, bool>
    {
        private readonly IRepository<Addresses> _repository;

        public DeleteCartCommandHandler(IRepository<Addresses> repository)
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
