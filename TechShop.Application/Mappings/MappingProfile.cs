using AutoMapper;
using TechShop.Application.Features.Address.CreateAddresses;
using TechShop.Application.Features.Address.UpdateAddresses;
using TechShop.Application.Features.Auth.Register;
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
using TechShop.Application.Features.Products.CreateFullProduct;
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
using TechShop.Domain.DTOs.Register;
using TechShop.Domain.DTOs.Users;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Domain.Entities;
using TechShop.TechShop.Domain.Entities;
using TechShop.TechShop.Domain.Enums;
using Stripe;
using Stripe.Checkout;
using TechShop.Application.Features.Cart.UpdateCart;

namespace TechShop.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Payments & Order Details
            CreateMap<Session, OrderDetails>()
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.CustomerEmail))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(_ => 0)) 
                 .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => (int)((src.AmountTotal ?? 0) / 100)));
            // Register
            CreateMap<RegisterDto, Users>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
               .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Username))
               .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
               .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Addresses
            CreateMap<CreateAddressesDto, Addresses>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UpdateAddressesDto, Addresses>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<Addresses, AddressesDto>().ReverseMap();

            // Cart
            CreateMap<UpdateCartDto, Cart>();
            CreateMap<CreateFullCartDto, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateCartDto, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateCartCommand, Cart>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Cart, CartDto>().ReverseMap();

            // CartItem
            CreateMap<CreateFullCartItemDto, CartItem>();
            CreateMap<CreateCartItemDto, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateCartItemCommand, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<UpdateCartItemDto, CartItem>();

            // Categories
            CreateMap<CreateCategoriesDto, Categories>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateCategoriesCommand, Categories>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateCategoriesDto, Categories>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Categories, CategoriesDto>().ReverseMap();

            // OrderDetails
            CreateMap<CreateOrderDetailsDto, OrderDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateOrderDetailsCommand, OrderDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UpdateOrderDetailsDto, OrderDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

            // OrderItem
            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateOrderItemCommand, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            // Payments
            CreateMap<CreatePaymentDto, Payments>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreatePaymentsCommand, Payments>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UpdatePaymentStatusDto, Payments>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Payments, PaymentsDto>().ReverseMap();

            // Full Product
            CreateMap<FullProductDto, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<ProductsSkusDto, ProductsSkus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());
            CreateMap<ProductSkuAttributesDto, ProductSkuAttributes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // Products
            CreateMap<CreateProductDto, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateProductsCommand, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UpdateProductDto, Products>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<Products, ProductsDto>().ReverseMap();

            // ProductSkuAttributes
            CreateMap<CreateProductSkuAttributesDto, ProductSkuAttributes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateProductsSkuAttributesCommand, ProductSkuAttributes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateProductsSkuAttributesDto, ProductSkuAttributes>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProductSkuAttributes, ProductSkuAttributesDto>().ReverseMap();

            // ProductsSkus
            CreateMap<CreateProductsSkusDto, ProductsSkus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateProductsSkusCommand, ProductsSkus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateProductsSkusDto, ProductsSkus>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<ProductsSkus, ProductsSkusDto>().ReverseMap();

            // Users
            CreateMap<CreateAddressForNewUser, Addresses>();
            CreateMap<CreateUserDto, Users>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateUsersCommand, Users>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UpdateUserDto, Users>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<Users, UserDto>().ReverseMap();

            // Wishlist
            CreateMap<CreateWishlistDto, Wishlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateWishlistCommand, Wishlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<UpdateWishlistDto, Wishlist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<Wishlist, WishlistDto>().ReverseMap();
        }
    }
}
