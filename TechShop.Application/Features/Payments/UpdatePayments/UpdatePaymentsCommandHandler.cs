using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.UpdatePayments
{
    public class UpdatePaymentsCommandHandler : IRequestHandler<UpdatePaymentsCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Payments> _repository;
        private readonly IMapper _mapper;

        public UpdatePaymentsCommandHandler(IRepository<Domain.Entities.Payments> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePaymentsCommand request, CancellationToken cancellationToken)
        {
            var payments = await _repository.GetByIdAsync(request.id);
            if (payments == null) return false;
            _mapper.Map(request, payments);
            payments.ModifiedAt = DateTime.UtcNow;
            payments.ModifiedBy = "System";
            await _repository.UpdateAsync(payments);
            return true;
        }
    }
}
