using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAllAddresses
{
    public class GetAllCartQueryHandler : IRequestHandler<GetAllCartItemQuery, IEnumerable<AddressesDto>>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;

        public GetAllCartQueryHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressesDto>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressesDto>>(addresses);
        }
    }
}
