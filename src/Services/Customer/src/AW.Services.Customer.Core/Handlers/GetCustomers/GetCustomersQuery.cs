using Ardalis.Result;
using AW.SharedKernel.Interfaces;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetCustomers;

public class GetCustomersQuery : IRequest<Result<List<Customer>>>
{
    public GetCustomersQuery() { }

    public GetCustomersQuery(CustomerType? customerType, bool includeDetails = false)
    {
        CustomerType = customerType;
        IncludeDetails = includeDetails;
    }

    public CustomerType? CustomerType { get; private init; }
    public bool IncludeDetails { get; private init; }
}
