using AutoMapper;
using TechShop.Application.Features.Address.CreateAddresses;
using TechShop.Application.Features.Address.UpdateAddresses;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Cart;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Domain.DTOs.Categories;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Domain.DTOs.Payments;
using TechShop.Domain.DTOs.Products;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Domain.DTOs.Users;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Domain.Entities;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateAddressesCommand, Addresses>()
             .ForMember(dest => dest.Id, opt => opt.Ignore()) 
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<Addresses, AddressesDto>().ReverseMap();
            CreateMap<Addresses, CreateAddressesDto>().ReverseMap();
            CreateMap<Addresses, UpdateAddressesDto>().ReverseMap();

            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<Cart, CreateCartDto>().ReverseMap();
            CreateMap<Cart, UpdateCartDto>().ReverseMap();

            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<CartItem, CreateCartItemDto>().ReverseMap();
            CreateMap<CartItem, UpdateCartDto>().ReverseMap();

            CreateMap<Categories, CategoriesDto>().ReverseMap();
            CreateMap<Categories, CreateCategoriesDto>().ReverseMap();
            CreateMap<Categories, UpdateCategoriesDto>().ReverseMap();

            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
            CreateMap<OrderDetails, CreateOrderDetailsDto>().ReverseMap();
            CreateMap<OrderDetails, UpdateOrderDetailsDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();

            CreateMap<Payments, PaymentsDto>().ReverseMap();
            CreateMap<Payments, CreatePaymentDto>().ReverseMap();
            CreateMap<Payments, UpdatePaymentStatusDto>().ReverseMap();

            CreateMap<Products, ProductsDto>().ReverseMap();
            CreateMap<Products, CreateProductDto>().ReverseMap();
            CreateMap<Products, UpdateProductDto>().ReverseMap();

            CreateMap<ProductSkuAttributes, ProductSkuAttributesDto>().ReverseMap();
            CreateMap<ProductSkuAttributes, CreateProductSkuAttributesDto>().ReverseMap();
            CreateMap<ProductSkuAttributes, UpdateProductsSkuAttributesDto>().ReverseMap();

            CreateMap<ProductsSkus, ProductsSkusDto>().ReverseMap();
            CreateMap<ProductsSkus, CreateProductsSkusDto>().ReverseMap();
            CreateMap<ProductsSkus, UpdateProductsSkusDto>().ReverseMap();

            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, CreateUserDto>().ReverseMap();
            CreateMap<Users, UpdateUserDto>().ReverseMap();

            CreateMap<Wishlist, WishlistDto>().ReverseMap();
            CreateMap<Wishlist, CreateWishlistDto>().ReverseMap();
            CreateMap<Wishlist, UpdateWishlistDto>().ReverseMap();
        }
    }
}
