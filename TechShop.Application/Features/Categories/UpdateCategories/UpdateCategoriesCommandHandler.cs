using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateCategories
{
    public class UpdateCategoriesCommandHandler : IRequestHandler<UpdateCategoriesCommand, bool>
    {
        private readonly IRepository<Categories> _repository;
        private readonly IMapper _mapper;

        public UpdateCategoriesCommandHandler(IRepository<Categories> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var address = await _repository.GetByIdAsync(request.id);
            if (address == null) return false;

            _mapper.Map(request, address);
            await _repository.UpdateAsync(address);
            return true;
        }
    }
}
