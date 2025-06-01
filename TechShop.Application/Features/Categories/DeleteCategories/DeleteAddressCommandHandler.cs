using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteCategories
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCategoriesCommand, bool>
    {
        private readonly IRepository<Categories> _repository;

        public DeleteCartCommandHandler(IRepository<Categories> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCategoriesCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;

            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
