using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateProductsSkus
{
    public class UpdateProductsSkusCommandHandler : IRequestHandler<UpdateProductsSkusCommand, bool>
    {
        private readonly IRepository<ProductsSkus> _repository;
        private readonly IMapper _mapper;

        public UpdateProductsSkusCommandHandler(IRepository<ProductsSkus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductsSkusCommand request, CancellationToken cancellationToken)
        {
            var productsSkus = await _repository.GetByIdAsync(request.id);
            if (productsSkus == null) return false;
            _mapper.Map(request, productsSkus);
            await _repository.UpdateAsync(productsSkus);
            return true;
        }
    }
}
