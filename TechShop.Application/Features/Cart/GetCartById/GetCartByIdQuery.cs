using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Cart;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetCartById
{
    public record GetCartByIdQuery(int id) : IRequest<CartDto?>;
}
