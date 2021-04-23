using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Application.StateProvince.GetStateProvinces
{
    public class GetStateProvincesQuery : IRequest<List<StateProvince>>
    {
        public string CountryRegionCode { get; set; }
    }
}