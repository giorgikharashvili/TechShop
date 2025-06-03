using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.GetAllPayments
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentsDto>>
    {
        private readonly IRepository<Domain.Entities.Payments> _repository;
        private readonly IMapper _mapper;

        public GetAllPaymentsQueryHandler(IRepository<Domain.Entities.Payments> repository, IMapper mapper)
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
