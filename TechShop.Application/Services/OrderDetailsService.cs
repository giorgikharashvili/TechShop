using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services
{
    public class OrderDetailsService : IEntityService<OrderDetailsDto, CreateOrderDetailsDto, UpdateOrderDetailsDto>
    {
        private readonly IRepository<OrderDetails> _orderDetailsRepository;

        public OrderDetailsService(IRepository<OrderDetails> orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<OrderDetailsDto> CreateAsync(CreateOrderDetailsDto dto)
        {
            var order = new OrderDetails
            {
                
                UserId = dto.UserId,
                TotalPrice = dto.TotalPrice,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _orderDetailsRepository.AddAsync(order);
            return MapToDto(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _orderDetailsRepository.GetByIdAsync(id);
            if (order == null) return false;

            await _orderDetailsRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<OrderDetailsDto>> GetAllAsync()
        {
            var orders = await _orderDetailsRepository.GetAllAsync();
            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrderDetailsDto> GetByIdAsync(int id)
        {
            var order = await _orderDetailsRepository.GetByIdAsync(id);
            if (order == null) throw new KeyNotFoundException();
            return MapToDto(order);
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrderDetailsDto dto)
        {
            var order = await _orderDetailsRepository.GetByIdAsync(id);
            if (order == null) return false;

            order.TotalPrice = dto.TotalPrice;
            order.ModifiedAt = DateTime.UtcNow;
            order.ModifiedBy = "System";

            await _orderDetailsRepository.UpdateAsync(order);
            return true;
        }

        private OrderDetailsDto MapToDto(OrderDetails entity)
        {
            return new OrderDetailsDto
            {
                UserId = entity.UserId,
                TotalPrice = entity.TotalPrice,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
