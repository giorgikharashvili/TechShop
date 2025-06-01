using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateCategories
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
    {
        private readonly IRepository<Categories> _repository;
        private readonly IMapper _mapper;
        
        public CreateCartItemCommandHandler(IRepository<Categories> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Categories>(request);
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
