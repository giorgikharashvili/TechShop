using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.GetPaymentsById;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Payments;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetPaymentsById
{
    public class GetPaymentsByIdQueryHandler : IRequestHandler<GetPaymentsByIdQuery, PaymentsDto?>
    {
        private readonly IRepository<Payments> _repository;
        private readonly IMapper _mapper;

        public GetPaymentsByIdQueryHandler(IRepository<Payments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentsDto?> Handle(GetPaymentsByIdQuery request, CancellationToken cancellationToken)
        {
            var payments = await _repository.GetByIdAsync(request.id);
            if (payments == null) return null;
            return _mapper.Map<PaymentsDto>(payments);
        }
    }
}
