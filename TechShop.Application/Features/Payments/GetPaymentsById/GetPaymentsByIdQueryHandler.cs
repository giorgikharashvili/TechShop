using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.GetPaymentsById
{
    public class GetPaymentsByIdQueryHandler : IRequestHandler<GetPaymentsByIdQuery, PaymentsDto?>
    {
        private readonly IRepository<Domain.Entities.Payments> _repository;
        private readonly IMapper _mapper;

        public GetPaymentsByIdQueryHandler(IRepository<Domain.Entities.Payments> repository, IMapper mapper)
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
