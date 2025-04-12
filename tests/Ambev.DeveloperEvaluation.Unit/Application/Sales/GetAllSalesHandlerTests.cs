using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;

public class GetAllSalesHandlerTests
{
    private readonly Mock<ISalesRepository> _salesRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllSalesHandler _handler;

    public GetAllSalesHandlerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _salesRepositoryMock = new Mock<ISalesRepository>();
        _handler = new GetAllSalesHandler(_salesRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsListOfSales()
    {
        // Arrange
        var salesList = new List<Sale>
        {
            new Sale { Id = Guid.NewGuid(), Branch = "Branch 1" },
            new Sale { Id = Guid.NewGuid(), Branch = "Branch 2" }
        };
        _salesRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(salesList);

        // Ensure the mapper correctly maps the Sale entities to GetAllSalesResult
        _mapperMock.Setup(m => m.Map<List<GetAllSalesResult>>(salesList)).Returns(new List<GetAllSalesResult>
        {
            new GetAllSalesResult { Id = salesList[0].Id, Branch = salesList[0].Branch },
            new GetAllSalesResult { Id = salesList[1].Id, Branch = salesList[1].Branch }
        });

        var request = new GetAllSalesQuery();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().ContainSingle(r => r.Branch == "Branch 1");
        result.Should().ContainSingle(r => r.Branch == "Branch 2");
        _salesRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}