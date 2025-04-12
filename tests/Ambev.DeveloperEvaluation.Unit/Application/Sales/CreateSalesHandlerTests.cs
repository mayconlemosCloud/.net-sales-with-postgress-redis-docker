using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;

public class CreateSalesHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ISalesRepository> _salesRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly CreateSalesHandler _handler;

    public CreateSalesHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _salesRepositoryMock = new Mock<ISalesRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new CreateSalesHandler(_mapperMock.Object, _salesRepositoryMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreateSalesResult()
    {
        // Arrange
        var request = new CreateSalesCommand
        {
            CreatedAt = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            Branch = "Test Branch",
            Items = new List<CreateSalesItemCommand>
            {
                new CreateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 2 }
            },
            IsCancelled = false
        };

        // Ensure the ProductId in the request matches the ProductId in the mocked product list
        var productId = request.Items.First().ProductId;
        var allProducts = new List<Product>
        {
            new Product { Id = productId, Name = "Test Product", UnitPrice = 10.0m }
        };
        _productRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(allProducts);

        var saleEntity = new Sale { Id = Guid.NewGuid() };
        _mapperMock.Setup(m => m.Map<Sale>(request)).Returns(saleEntity);
        _salesRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(saleEntity);


        _salesRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(saleEntity);


        _mapperMock.Setup(m => m.Map<CreateSalesResult>(saleEntity)).Returns(new CreateSalesResult
        {
            SaleId = saleEntity.Id,
            CreatedAt = saleEntity.CreatedAt ?? DateTime.UtcNow,
            CustomerId = saleEntity.CustomerId,
            Branch = saleEntity.Branch,
            TotalAmount = saleEntity.TotalAmount,
            Discount = saleEntity.Discount,
            Items = saleEntity.Items
        });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.SaleId.Should().Be(saleEntity.Id);
        _salesRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}