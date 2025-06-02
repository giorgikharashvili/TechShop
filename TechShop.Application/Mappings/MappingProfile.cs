using AutoMapper;
using TechShop.Application.Features.Address.CreateAddresses;
using TechShop.Application.Features.Address.UpdateAddresses;
using TechShop.Application.Features.Cart.CreateCart;
using TechShop.Application.Features.CartItem.CreateCartItem;
using TechShop.Application.Features.Categories.CreateCategories;
using TechShop.Application.Features.Categories.UpdateCategories;
using TechShop.Application.Features.OrderDetails.CreateOrderDetails;
using TechShop.Application.Features.OrderDetails.UpdateOrderDetails;
using TechShop.Application.Features.OrderItem.CreateOrderItem;
using TechShop.Application.Features.OrderItem.UpdateOrderItem;
using TechShop.Application.Features.Payments.CreatePayments;
using TechShop.Application.Features.Payments.UpdatePayments;
using TechShop.Application.Features.Products.CreateProducts;
using TechShop.Application.Features.Products.UpdateProducts;
using TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkus.CreateProductsSkus;
using TechShop.Application.Features.ProductsSkus.UpdateProductsSkus;
using TechShop.Application.Features.Users.CreateUsers;
using TechShop.Application.Features.Users.UpdateUsers;
using TechShop.Application.Features.Wishlist.CreateWishlist;
using TechShop.Application.Features.Wishlist.UpdateWishlist;
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

            CreateMap<CreateAddressCommand, Addresses>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<Addresses, AddressesDto>().ReverseMap();

            CreateMap<CreateCartCommand, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
                
            CreateMap<Cart, CartDto>().ReverseMap();

            CreateMap<CreateCartItemCommand, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CartItem, CartItemDto>().ReverseMap();

            CreateMap<CreateCategoriesCommand, Categories>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateCategoriesCommand, Categories>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Categories, CategoriesDto>().ReverseMap();

            CreateMap<CreateOrderDetailsCommand, OrderDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdateOrderDetailsCommand, OrderDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

            CreateMap<CreateOrderItemCommand, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
             

            CreateMap<UpdateOrderItemCommand, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<CreatePaymentsCommand, Payments>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdatePaymentsCommand, Payments>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<Payments, PaymentsDto>().ReverseMap();

            CreateMap<CreateProductsCommand, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdateProductsCommand, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<Products, ProductsDto>().ReverseMap();

            CreateMap<CreateProductsSkuAttributesCommand, ProductSkuAttributes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateProductsSkuAttributesCommand, ProductSkuAttributes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProductSkuAttributes, ProductSkuAttributesDto>().ReverseMap();

            CreateMap<CreateProductsSkusCommand, ProductsSkus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateProductsSkusCommand, ProductsSkus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProductsSkus, ProductsSkusDto>().ReverseMap();

            CreateMap<CreateUsersCommand, Users>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdateUsersCommand, Users>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<Users, UserDto>().ReverseMap();

            CreateMap<CreateWishlistCommand, Wishlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<UpdateWishlistCommand, Wishlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<Wishlist, WishlistDto>().ReverseMap();
        }
    }
}