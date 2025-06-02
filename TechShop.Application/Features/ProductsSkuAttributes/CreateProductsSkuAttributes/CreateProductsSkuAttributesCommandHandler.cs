using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes
{
    public class CreateProductsSkuAttributesCommandHandler : IRequestHandler<CreateProductsSkuAttributesCommand, ProductSkuAttributesDto>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;
        private readonly IMapper _mapper;
        
        public CreateProductsSkuAttributesCommandHandler(IRepository<ProductSkuAttributes> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ProductSkuAttributesDto> Handle(CreateProductsSkuAttributesCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductSkuAttributes>(request);
            await _repository.AddAsync(entity);
            var dto = _mapper.Map<ProductSkuAttributesDto>(entity);
            return dto;
        }
    }
}
