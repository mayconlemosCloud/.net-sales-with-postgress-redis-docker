using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    public class SaleTests
    {
        [Fact]
        public void AddItem_ShouldCalculateTotalAmountCorrectly()
        {
            // Arrange
            var sale = new Sale();
            var product = new Product { Id = 1, Name = "Product A", UnitPrice = 10.0m };
            var saleItem = new SaleItem { Product = product, Quantity = 5, Discount = 1.0m };

            // Act
            sale.AddItem(saleItem);

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal(45.0m, sale.TotalAmount); // (10 - 1) * 5
        }

        [Fact]
        public void AddItem_ShouldThrowException_WhenQuantityIsZeroOrNegative()
        {
            // Arrange
            var sale = new Sale();
            var product = new Product { Id = 1, Name = "Product A", UnitPrice = 10.0m };
            var saleItem = new SaleItem { Product = product, Quantity = 0, Discount = 1.0m };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sale.AddItem(saleItem));
        }

        [Fact]
        public void AddItem_ShouldThrowException_WhenDiscountIsInvalid()
        {
            // Arrange
            var sale = new Sale();
            var product = new Product { Id = 1, Name = "Product A", UnitPrice = 10.0m };
            var saleItem = new SaleItem { Product = product, Quantity = 5, Discount = 15.0m };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sale.AddItem(saleItem));
        }

        [Fact]
        public void CancelSale_ShouldSetIsCancelledToTrue()
        {
            // Arrange
            var sale = new Sale();

            // Act
            sale.CancelSale();

            // Assert
            Assert.True(sale.IsCancelled);
        }
    }
}