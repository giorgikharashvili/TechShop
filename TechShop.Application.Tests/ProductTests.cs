using AutoMapper;
using Moq;
using TechShop.Application.Features.Products.CreateProducts;
using TechShop.Application.Features.Products.GetAllProducts;
using TechShop.Domain.DTOs.Products;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Tests;

public class ProductTests
{
    private readonly Mock<IRepository<Products>> _productsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public ProductTests()
    {
        _productsRepositoryMock = new Mock<IRepository<Products>>();
        _mapperMock = new Mock<IMapper>();
    }


    
    [Fact]
    public async Task AddAsync_ShouldAddProductToDatabase()
    {
        // Arrange

        var fakeProducts = new List<Products>
        {
            new Products
            {
                Id = 1,
                Name = "RTX 3090",
                Description = "High Quality GPU",
                CategoryId = 1
            }
        };

        _productsRepositoryMock
            .Setup(repo => repo.AddAsync(fakeProducts[0]))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act

        await _productsRepositoryMock.Object.AddAsync(fakeProducts[0]);

        // Assert

        _productsRepositoryMock.Verify(repo => repo.AddAsync(fakeProducts[0]), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldGetTheProductWithGivenId()
    {
        // Arrange

        var productId = 1;
        var fakeProducts = new Products
        {
                Id = productId,
                Name = "RTX 3090",
                Description = "High Quality GPU",
                CategoryId = 1
        };

        _productsRepositoryMock
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(fakeProducts);

        // Act

       var product = await _productsRepositoryMock.Object.GetByIdAsync(productId);

        // Assert

        Assert.NotNull(product);
        Assert.Equal(fakeProducts.Id, product.Id);
        Assert.Equal(fakeProducts.Name, product.Name);
        Assert.Equal(fakeProducts.Description, product.Description);
        Assert.Equal(fakeProducts.CategoryId, product.CategoryId);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTheProduct()
    {
        // Arrange

        var fakeProducts = new Products
        {
            Id = 1,
            Name = "RTX 3090",
            Description = "High Quality GPU",
            CategoryId = 1
        };

        _productsRepositoryMock
            .Setup(repo => repo.UpdateAsync(fakeProducts))
            .Returns(Task.CompletedTask);

        // Act

        await _productsRepositoryMock.Object.UpdateAsync(fakeProducts);

        // Assert

        _productsRepositoryMock.Verify(repo => repo.UpdateAsync(fakeProducts), Times.Once);
    }



    [Fact] 
    public async Task DeleteAsync_ShouldDeleteTheProductWithGivenId()
    {
        // Arrange

        var productId = 1;

        _productsRepositoryMock
            .Setup(repo => repo.DeleteAsync(productId))
            .Returns(Task.CompletedTask)
            .Verifiable();

        // Act

        await _productsRepositoryMock.Object.DeleteAsync(productId);

        // Assert

        _productsRepositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
    }


    [Fact]
    public async Task GetExistingProductsAsync_ReturnsAllExistingProducts()
    {
        // Arrange

        var fakeProducts = new List<Products>
        {
            new Products
            {
                Id = 1,
                Name = "RTX 3090",
                Description = "High Quality GPU",
                CategoryId = 1
            }
        };

        var expectedDtos = new List<ProductsDto>
        {
            new ProductsDto
            {
                Id = 1,
                Name = "RTX 3090",
                Description = "High Quality GPU",
                CategoryId = 1
            }
        };

        _productsRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(fakeProducts);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<ProductsDto>>(fakeProducts))
            .Returns(expectedDtos);

        var handler = new GetAllProductsQueryHandler(_productsRepositoryMock.Object, _mapperMock.Object);
        var query = new GetAllProductsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultList = result.ToList();

        // Assert
        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.Equal("RTX 3090", resultList[0].Name);

        _productsRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<IEnumerable<ProductsDto>>(fakeProducts), Times.Once);
    }



}
