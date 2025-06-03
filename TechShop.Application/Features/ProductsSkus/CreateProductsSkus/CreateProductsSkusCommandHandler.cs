using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.ProductsSkus.CreateProductsSkus
{
    public class CreateProductsSkusCommandHandler : IRequestHandler<CreateProductsSkusCommand, ProductsSkusDto>
    {
        private readonly IRepository<Domain.Entities.ProductsSkus> _repository;
        private readonly IMapper _mapper;
        
        public CreateProductsSkusCommandHandler(IRepository<Domain.Entities.ProductsSkus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ProductsSkusDto> Handle(CreateProductsSkusCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.ProductsSkus>(request.Dto);
            await _repository.AddAsync(entity);

            var dto = _mapper.Map<ProductsSkusDto>(entity);

            return dto;
        }
    }
}
