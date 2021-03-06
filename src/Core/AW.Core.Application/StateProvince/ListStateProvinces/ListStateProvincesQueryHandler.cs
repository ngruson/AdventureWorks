﻿using Ardalis.Specification;
using AutoMapper;
using AW.Core.Application.Specifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.StateProvince.ListStateProvinces
{
    public class ListStateProvincesQueryHandler : IRequestHandler<ListStateProvincesQuery, IEnumerable<StateProvinceDto>>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryBase<Domain.Person.StateProvince> repository;

        public ListStateProvincesQueryHandler(IMapper mapper, IRepositoryBase<Domain.Person.StateProvince> repository)
            => (this.mapper, this.repository) = (mapper, repository);

        public async Task<IEnumerable<StateProvinceDto>> Handle(ListStateProvincesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Person.StateProvince> stateProvinces;
            if (!string.IsNullOrEmpty(request.CountryRegionCode))
                stateProvinces = await repository.ListAsync(
                    new ListStateProvincesSpecification(request.CountryRegionCode)
                );
            else
                stateProvinces = await repository.ListAsync();

            return mapper.Map<IEnumerable<StateProvinceDto>>(stateProvinces);
        }
    }
}