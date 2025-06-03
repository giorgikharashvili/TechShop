using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAllAddresses
{
    public class GetAllAddressQueryHandler : IRequestHandler<GetAllAddressQuery, IEnumerable<AddressesDto>>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;

        public GetAllAddressQueryHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressesDto>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<AddressesDto>>(addresses);
        }
    }
}
