using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public class CreateAddressesCommandHandler : IRequestHandler<CreateAddressesCommand, int>
    {
        private readonly IRepository<Addresses> _repository;
        private readonly IMapper _mapper;
        
        public CreateAddressesCommandHandler(IRepository<Addresses> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateAddressesCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Addresses>(request);
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
