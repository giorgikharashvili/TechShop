using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Categories;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetCategoriesById
{
    public class GetCategoriesByIdQueryHandler : IRequestHandler<GetCategoriesByIdQuery, CategoriesDto?>
    {
        private readonly IRepository<Categories> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesByIdQueryHandler(IRepository<Categories> repository, IMapper mapper)
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
