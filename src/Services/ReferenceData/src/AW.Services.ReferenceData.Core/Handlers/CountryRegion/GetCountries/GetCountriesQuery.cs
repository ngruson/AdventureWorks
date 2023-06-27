using Ardalis.Result;
using MediatR;

namespace AW.Services.ReferenceData.Core.Handlers.CountryRegion.GetCountries;

public class GetCountriesQuery : IRequest<Result<List<Country>>>
{
}
