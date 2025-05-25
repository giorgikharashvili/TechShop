using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;

namespace TechShop.Application.Services
{
    public class OrderItemService : IEntityService<OrderItemDto, CreateOrderItemDto, UpdateOrderItemDto>
    {
        private readonly OrderItemRepository _orderItemRepository;

        public OrderItemService(OrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItemDto> CreateAsync(CreateOrderItemDto dto)
        {
            var item = new OrderItem
            {
                ProductId = dto.ProductId,
                ProductsSkuId = dto.ProductsSkuId,
                Quantity = dto.Quantity
            };

            await _orderItemRepository.AddAsync(item);
            return MapToDto(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _orderItemRepository.GetByIdAsync(id);
            if (item == null) return false;

            await _orderItemRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<OrderItemDto>> GetAllAsync()
        {
            var items = await _orderItemRepository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<OrderItemDto> GetByIdAsync(int id)
        {
            var item = await _orderItemRepository.GetByIdAsync(id);
            if (item == null) throw new KeyNotFoundException();

            return MapToDto(item);
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrderItemDto dto)
        {
            var item = await _orderItemRepository.GetByIdAsync(id);
            if (item == null) return false;

            item.Quantity = dto.Quantity;
            await _orderItemRepository.UpdateAsync(item);
            return true;
        }

        private OrderItemDto MapToDto(OrderItem entity)
        {
            return new OrderItemDto
            {
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                ProductsSkuId = entity.ProductsSkuId,
                Quantity = entity.Quantity
            };
        }
    }
}
