using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteAddresses
{
    public class DeleteAddressesCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
    {
        private readonly IRepository<Addresses> _repository;

        public DeleteAddressesCommandHandler(IRepository<Addresses> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;

            await _repository.DeleteAsync(request.id);

            return true;
        }
    }
}
