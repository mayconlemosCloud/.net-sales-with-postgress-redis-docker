using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;

public class GetSalesHandlerTests
{
    private readonly Mock<ISalesRepository> _salesRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetSalesHandler _handler;

    public GetSalesHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _salesRepositoryMock = new Mock<ISalesRepository>();
        _handler = new GetSalesHandler(_salesRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSalesDetails()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var saleEntity = new Sale { Id = saleId, Branch = "Test Branch" };
        _salesRepositoryMock.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync(saleEntity);

        // Ensure the mapper correctly maps the Sale entity to GetSalesResult
        _mapperMock.Setup(m => m.Map<GetSalesResult>(saleEntity)).Returns(new GetSalesResult
        {
            Id = saleEntity.Id,
            Branch = saleEntity.Branch,
            CreatedAt = saleEntity.CreatedAt ?? DateTime.UtcNow,
            CustomerId = saleEntity.CustomerId,
            TotalAmount = saleEntity.TotalAmount,
            Discount = saleEntity.Discount,
            Items = saleEntity.Items
        });

        var request = new GetSalesCommand { SaleId = saleId };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
        result.Branch.Should().Be("Test Branch");
        _salesRepositoryMock.Verify(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
    }
}