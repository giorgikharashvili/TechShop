using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.UpdateCategories
{
    public class UpdateCategoriesCommandHandler : IRequestHandler<UpdateCategoriesCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Categories> _repository;
        private readonly IMapper _mapper;

        public UpdateCategoriesCommandHandler(IRepository<Domain.Entities.Categories> repository, IMapper mapper)
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
