using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAddressesById
{
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressesByIdQuery, AddressesDto?>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;

        public GetAddressByIdQueryHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AddressesDto?> Handle(GetAddressesByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _repository.GetByIdAsync(request.id);
            if (address == null) return null;
            return _mapper.Map<AddressesDto>(address);
        }
    }
}
