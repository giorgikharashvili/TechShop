using AutoMapper;
using MediatR;
using TechShop.Application.Features.Address.UpdateProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    public class UpdateProductsSkuAttributesCommandHandler : IRequestHandler<UpdateProductsSkuAttributesCommand, bool>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;
        private readonly IMapper _mapper;

        public UpdateProductsSkuAttributesCommandHandler(IRepository<ProductSkuAttributes> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductsSkuAttributesCommand request, CancellationToken cancellationToken)
        {
            var productSkuAttributes = await _repository.GetByIdAsync(request.id);
            if (productSkuAttributes == null) return false;
            _mapper.Map(request, productSkuAttributes);
            await _repository.UpdateAsync(productSkuAttributes);
            return true;
        }
    }
}
