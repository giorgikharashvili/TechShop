using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.UpdateOrderDetails;

public class UpdateOrderDetailsCommandHandler(
    IRepository<Domain.Entities.OrderDetails> _repository,
    IMapper _mapper,
    ILogger<UpdateOrderDetailsCommandHandler> _logger
    ) : IRequestHandler<UpdateOrderDetailsCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateOrderDetailsCommand for OrderDetails ID: {Id}", request.id);

        var orderDetails = await _repository.GetByIdAsync(request.id);
        if (orderDetails == null)
        {
            _logger.LogWarning("OrderDetails with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request.Dto, orderDetails);
        _logger.LogInformation("Mapped update request to existing OrderDetails entity.");

        orderDetails.ModifiedAt = DateTime.UtcNow;
        orderDetails.ModifiedBy = "System";

        await _repository.UpdateAsync(orderDetails);
        _logger.LogInformation("OrderDetails with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
