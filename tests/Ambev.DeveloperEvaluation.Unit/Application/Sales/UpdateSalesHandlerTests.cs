using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;

public class UpdateSalesHandlerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ISalesRepository> _salesRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdateSalesHandler _handler;

    public UpdateSalesHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _salesRepositoryMock = new Mock<ISalesRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _handler = new UpdateSalesHandler(_mapperMock.Object, _salesRepositoryMock.Object, _productRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldUpdateSaleSuccessfully()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new UpdateSalesCommand
        {
            Id = saleId,
            Branch = "Test Branch",
            Items = new List<UpdateSalesItemCommand>
            {
                new UpdateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 5 }
            }
        };

        var sale = new Sale { Id = saleId };
        var product = new Product { Id = command.Items.First().ProductId, UnitPrice = 10.0m };

        _salesRepositoryMock.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        _productRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product> { product });
        _salesRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        _mapperMock.Setup(m => m.Map<UpdateSalesResult>(It.IsAny<Sale>())).Returns(new UpdateSalesResult
        {
            SaleId = sale.Id,
            Branch = sale.Branch,
            TotalAmount = sale.TotalAmount,
            TotalDiscount = sale.Discount,
            Items = sale.Items
        });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        _salesRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ShouldThrowValidationException()
    {
        // Arrange
        var command = new UpdateSalesCommand { Id = Guid.Empty, Branch = "Test Branch" };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
        _salesRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_SaleNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new UpdateSalesCommand { Id = saleId, Branch = "Test Branch" };

        _salesRepositoryMock.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync((Sale)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Sale with ID {saleId} not found.");
        _salesRepositoryMock.Verify(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldThrowException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new UpdateSalesCommand
        {
            Id = saleId,
            Branch = "Test Branch",
            Items = new List<UpdateSalesItemCommand>
            {
                new UpdateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 5 }
            }
        };

        var sale = new Sale { Id = saleId };

        _salesRepositoryMock.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        _productRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage($"Product with ID {command.Items.First().ProductId} not found.");
        _productRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_UpdateFails_ShouldThrowException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new UpdateSalesCommand
        {
            Id = saleId,
            Branch = "Test Branch",
            Items = new List<UpdateSalesItemCommand>
            {
                new UpdateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 5 }
            }
        };

        var sale = new Sale { Id = saleId };
        var product = new Product { Id = command.Items.First().ProductId, UnitPrice = 10.0m };

        _salesRepositoryMock.Setup(r => r.GetByIdAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        _productRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product> { product });
        _salesRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync((Sale)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Error updating sale");
        _salesRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}