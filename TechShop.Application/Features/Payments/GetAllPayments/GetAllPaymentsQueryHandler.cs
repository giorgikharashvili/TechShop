using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.GetAllPayments;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Payments;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAllAddresses
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentsDto>>
    {
        private readonly IRepository<Payments> _repository;
        private readonly IMapper _mapper;

        public GetAllPaymentsQueryHandler(IRepository<Payments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentsDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentsDto>>(payments);
        }
    }
}
