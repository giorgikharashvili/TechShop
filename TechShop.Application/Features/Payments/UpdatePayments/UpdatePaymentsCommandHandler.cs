using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdatePayments
{
    public class UpdatePaymentsCommandHandler : IRequestHandler<UpdatePaymentsCommand, bool>
    {
        private readonly IRepository<Payments> _repository;
        private readonly IMapper _mapper;

        public UpdatePaymentsCommandHandler(IRepository<Payments> repository, IMapper mapper)
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
