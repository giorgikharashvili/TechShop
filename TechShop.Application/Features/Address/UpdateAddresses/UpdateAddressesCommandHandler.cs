using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressesCommand, bool>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;

        public UpdateAddressCommandHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAddressesCommand request, CancellationToken cancellationToken)
        {
            var address = await _repository.GetByIdAsync(request.id);
            if (address == null) return false;

            _mapper.Map(request, address);

            address.ModifiedAt = DateTime.UtcNow;
            address.ModifiedBy = "System";

            await _repository.UpdateAsync(address);
            return true;
        }
    }
}
