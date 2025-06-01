using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreatePayments
{
    public class CreatePaymentsCommandHandler : IRequestHandler<CreatePaymentsCommand, int>
    {
        private readonly IRepository<Payments> _repository;
        private readonly IMapper _mapper;
        
        public CreatePaymentsCommandHandler(IRepository<Payments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreatePaymentsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Payments>(request);
            entity.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
