using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales;

/// <summary>
/// Profile for mapping CreateSales entities
/// </summary>
public class CreateSalesProfile : Profile
{
    public CreateSalesProfile()
    {
        CreateMap<CreateSalesCommand, CreateSalesResult>();
        CreateMap<Sale, CreateSalesResult>();
        CreateMap<CreateSalesCommand, Sale>();
        CreateMap<CreateSalesItemCommand, SaleItem>();
    }
}