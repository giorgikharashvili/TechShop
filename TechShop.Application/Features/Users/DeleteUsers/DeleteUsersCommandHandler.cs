using MediatR;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.DeleteUsers
{
    public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand, bool>
    {
        private readonly IRepository<Domain.Entities.Users> _repository;

        public DeleteUsersCommandHandler(IRepository<Domain.Entities.Users> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.id);
            if (exists == null) return false;
            await _repository.DeleteAsync(request.id);
            return true;
        }
    }
}
