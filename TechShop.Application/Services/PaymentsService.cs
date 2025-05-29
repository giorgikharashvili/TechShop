using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Payments;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services
{
    public class PaymentsService : IEntityService<PaymentsDto, CreatePaymentDto, UpdatePaymentStatusDto>
    {
        private readonly IRepository<Payments> _paymentsRepository;

        public PaymentsService(IRepository<Payments> paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public async Task<PaymentsDto> CreateAsync(CreatePaymentDto dto)
        {
            var payment = new Payments
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _paymentsRepository.AddAsync(payment);
            return MapToDto(payment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _paymentsRepository.GetByIdAsync(id);
            if (payment == null) return false;

            await _paymentsRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<PaymentsDto>> GetAllAsync()
        {
            var payments = await _paymentsRepository.GetAllAsync();
            return payments.Select(MapToDto).ToList();
        }

        public async Task<PaymentsDto> GetByIdAsync(int id)
        {
            var payment = await _paymentsRepository.GetByIdAsync(id);
            if (payment == null) throw new KeyNotFoundException();
            return MapToDto(payment);
        }

        public async Task<bool> UpdateAsync(int id, UpdatePaymentStatusDto dto)
        {
            var payment = await _paymentsRepository.GetByIdAsync(id);
            if (payment == null) return false;

            payment.Status = dto.Status;
            payment.ModifiedAt = DateTime.UtcNow;
            payment.ModifiedBy = "System";

            await _paymentsRepository.UpdateAsync(payment);
            return true;
        }

        private PaymentsDto MapToDto(Payments entity)
        {
            return new PaymentsDto
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                StripePaymentId = entity.StripePaymentId,
                Amount = entity.Amount,
                Currency = entity.Currency,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
