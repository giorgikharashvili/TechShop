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

namespace TechShop.Application.Features.Address.CreateCartItem
{
    public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IMapper _mapper;
        
        public CreateCartItemCommandHandler(IRepository<CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CartItem>(request);
            await _repository.AddAsync(entity);
            return entity.Id;
        }
    }
}
