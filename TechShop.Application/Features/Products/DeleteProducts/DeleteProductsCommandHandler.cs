using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Products.DeleteProducts
{
    public class DeleteProductsCommandHandler : IRequestHandler<DeleteProductsCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Products> _repository;
        private readonly IMapper _mapper;

        public DeleteProductsCommandHandler(IRepository<Domain.Entities.Products> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteProductsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.id);
            if (entity == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}