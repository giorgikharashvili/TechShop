using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeletePayments
{
    public class DeletePaymentsCommandHandler : IRequestHandler<DeletePaymentsCommand, bool>
    {
        private readonly IRepository<Payments> _repository;

        public DeletePaymentsCommandHandler(IRepository<Payments> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePaymentsCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
