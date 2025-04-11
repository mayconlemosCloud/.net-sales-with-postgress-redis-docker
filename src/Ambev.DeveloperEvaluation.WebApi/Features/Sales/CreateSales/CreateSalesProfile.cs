using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

/// <summary>
/// Profile for mapping between Application and API CreateSales responses
/// </summary>
public class CreateSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSales feature
    /// </summary>
    public CreateSalesProfile()
    {
        CreateMap<CreateSalesRequest, CreateSalesCommand>();
        CreateMap<CreateSalesResult, CreateSalesResponse>();
    }
}