using AutoMapper;
using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.UpdateUsers
{
    public class UpdateUsersCommandHandler : IRequestHandler<UpdateUsersCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Users> _repository;
        private readonly IMapper _mapper;

        public UpdateUsersCommandHandler(IRepository<Domain.Entities.Users> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetByIdAsync(request.id);
            if (users == null) return false;

            _mapper.Map(request, users);

            users.ModifiedAt = DateTime.UtcNow;
            users.ModifiedBy = "System";

            await _repository.UpdateAsync(users);

            return true;
        }
    }
}
