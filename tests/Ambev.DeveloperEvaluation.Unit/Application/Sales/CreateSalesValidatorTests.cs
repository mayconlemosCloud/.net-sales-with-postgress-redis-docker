using Xunit;
using FluentValidation.TestHelper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using System;
using System.Collections.Generic;

public class CreateSalesValidatorTests
{
    private readonly CreateSalesValidator _validator;

    public CreateSalesValidatorTests()
    {
        _validator = new CreateSalesValidator();
    }

    [Fact]
    public void Validate_ValidCommand_ShouldPass()
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

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_MissingCustomerId_ShouldFail()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            CustomerId = Guid.Empty,
            Branch = "Test Branch",
            Items = new List<CreateSalesItemCommand>
            {
                new CreateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.CustomerId);
    }

    [Fact]
    public void Validate_EmptyBranch_ShouldFail()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            CustomerId = Guid.NewGuid(),
            Branch = string.Empty,
            Items = new List<CreateSalesItemCommand>
            {
                new CreateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Branch);
    }

    [Fact]
    public void Validate_EmptyItems_ShouldFail()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            CustomerId = Guid.NewGuid(),
            Branch = "Test Branch",
            Items = new List<CreateSalesItemCommand>()
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Items);
    }

    [Fact]
    public void Validate_InvalidItemQuantity_ShouldFail()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            CustomerId = Guid.NewGuid(),
            Branch = "Test Branch",
            Items = new List<CreateSalesItemCommand>
            {
                new CreateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 0 }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }

    [Fact]
    public void Validate_ExceedingItemQuantity_ShouldFail()
    {
        // Arrange
        var command = new CreateSalesCommand
        {
            CustomerId = Guid.NewGuid(),
            Branch = "Test Branch",
            Items = new List<CreateSalesItemCommand>
            {
                new CreateSalesItemCommand { ProductId = Guid.NewGuid(), Quantity = 21 }
            }
        };

        // Act & Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
    }
}