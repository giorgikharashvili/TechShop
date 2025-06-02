using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Categories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Categories.GetCategoriesById
{
    public class GetCategoriesByIdQueryHandler : IRequestHandler<GetCategoriesByIdQuery, CategoriesDto?>
    {
        private readonly IRepository<Domain.Entities.Categories> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesByIdQueryHandler(IRepository<Domain.Entities.Categories> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoriesDto?> Handle(GetCategoriesByIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByIdAsync(request.id);
            if (categories == null) return null;
            return _mapper.Map<CategoriesDto>(categories);
        }
    }
}
