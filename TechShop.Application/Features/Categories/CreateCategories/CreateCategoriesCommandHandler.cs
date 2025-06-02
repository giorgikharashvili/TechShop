using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Categories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.CreateCategories
{
    public class CreateCategoriesCommandHandler : IRequestHandler<CreateCategoriesCommand, CategoriesDto>
    {
        private readonly IRepository<Domain.Entities.Categories> _repository;
        private readonly IMapper _mapper;
        
        public CreateCategoriesCommandHandler(IRepository<Domain.Entities.Categories> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CategoriesDto> Handle(CreateCategoriesCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Categories>(request);
            await _repository.AddAsync(entity);
            var dto = _mapper.Map<CategoriesDto>(entity);
            return dto;
        }
    }
}
