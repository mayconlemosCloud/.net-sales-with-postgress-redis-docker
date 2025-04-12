using AutoMapper;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;

public class CreateSalesProfileTests
{
    private readonly IMapper _mapper;

    public CreateSalesProfileTests()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CreateSalesProfile>());
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void Map_CreateSalesCommand_To_Sale_ShouldBeValid()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            CustomerId = Guid.NewGuid(),
            Branch = "Test Branch",
            Items = new List<CreateSalesItemCommand>
            {
                new CreateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        // Act
        var result = _mapper.Map<Sale>(command);

        // Assert
        result.Should().NotBeNull();
        result.CustomerId.Should().Be(command.CustomerId);
        result.Branch.Should().Be(command.Branch);
        result.Items.Should().HaveCount(command.Items.Count);
    }

    [Fact]
    public void Map_Sale_To_CreateSalesResult_ShouldBeValid()
    {
        // Arrange
        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Branch = "Test Branch",
            Items = new List<SaleItem>
            {
                new SaleItem { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        // Act
        var result = _mapper.Map<CreateSalesResult>(sale);

        // Assert
        result.Should().NotBeNull();
        result.SaleId.Should().Be(sale.Id);
        result.CustomerId.Should().Be(sale.CustomerId);
        result.Branch.Should().Be(sale.Branch);
        result.Items.Should().HaveCount(sale.Items.Count);
    }
}