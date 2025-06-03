using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.CreatePayments
{
    public class CreatePaymentsCommandHandler : IRequestHandler<CreatePaymentsCommand, PaymentsDto>
    {
        private readonly IRepository<Domain.Entities.Payments> _repository;
        private readonly IMapper _mapper;
        
        public CreatePaymentsCommandHandler(IRepository<Domain.Entities.Payments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PaymentsDto> Handle(CreatePaymentsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Payments>(request.Dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(entity);

            var dto = _mapper.Map<PaymentsDto>(entity);

            return dto;
        }
    }
}
