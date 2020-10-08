using MediatR;
using System.Collections.Generic;

namespace AW.Application.StateProvince.ListStateProvinces
{
    public class ListStateProvincesQuery : IRequest<IEnumerable<StateProvinceDto>>
    {
        public string CountryRegionCode { get; set; }
    }
}