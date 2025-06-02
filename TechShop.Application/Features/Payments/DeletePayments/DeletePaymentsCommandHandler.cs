using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.DeletePayments
{
    public class DeletePaymentsCommandHandler : IRequestHandler<DeletePaymentsCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Payments> _repository;

        public DeletePaymentsCommandHandler(IRepository<Domain.Entities.Payments> repository)
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
