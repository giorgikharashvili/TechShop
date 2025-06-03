using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public class CreateAddressesCommandHandler : IRequestHandler<CreateAddressCommand, AddressesDto>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;
        
        public CreateAddressesCommandHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<AddressesDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Addresses>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(entity);

            var dto = _mapper.Map<AddressesDto>(entity);

            return dto;
        }
    }
}
