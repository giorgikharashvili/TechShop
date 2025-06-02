using AutoMapper;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.UpdateProducts
{
    public class UpdateProductsCommandHandler : IRequestHandler<UpdateProductsCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Products> _repository;
        private readonly IMapper _mapper;


        public UpdateProductsCommandHandler(IRepository<Domain.Entities.Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null) return false;
            _mapper.Map(request, entity);
            entity.ModifiedAt = DateTime.UtcNow;
            entity.ModifiedBy = "System";
            await _repository.UpdateAsync(entity);
            return true;
        }
    }
}