using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;

public class DeleteSalesHandlerTests
{
    private readonly Mock<ISalesRepository> _salesRepositoryMock;
    private readonly DeleteSalesHandler _handler;

    public DeleteSalesHandlerTests()
    {
        _salesRepositoryMock = new Mock<ISalesRepository>();
        _handler = new DeleteSalesHandler(_salesRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnSuccess()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new DeleteSalesCommand { SaleId = saleId };

        _salesRepositoryMock.Setup(r => r.DeleteAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        _salesRepositoryMock.Verify(r => r.DeleteAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ShouldThrowValidationException()
    {
        // Arrange
        var command = new DeleteSalesCommand { SaleId = Guid.Empty };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
        _salesRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_SaleNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new DeleteSalesCommand { SaleId = saleId };

        _salesRepositoryMock.Setup(r => r.DeleteAsync(saleId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Sales with ID {saleId} not found");
        _salesRepositoryMock.Verify(r => r.DeleteAsync(saleId, It.IsAny<CancellationToken>()), Times.Once);
    }
}