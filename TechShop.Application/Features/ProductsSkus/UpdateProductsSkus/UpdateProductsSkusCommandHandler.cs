using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;
namespace TechShop.Application.Features.ProductsSkus.UpdateProductsSkus
{
    public class UpdateProductsSkusCommandHandler : IRequestHandler<UpdateProductsSkusCommand, bool>
    {
        private readonly IRepository<Domain.Entities.ProductsSkus> _repository;
        private readonly IMapper _mapper;

        public UpdateProductsSkusCommandHandler(IRepository<Domain.Entities.ProductsSkus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductsSkusCommand request, CancellationToken cancellationToken)
        {
            var productsSkus = await _repository.GetByIdAsync(request.Id);
            if (productsSkus == null) return false;
            _mapper.Map(request, productsSkus);
            await _repository.UpdateAsync(productsSkus);
            return true;
        }
    }
}
