﻿using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.Customer
{
    public class StateProvinceDto : IMapFrom<Domain.Person.StateProvince>
    {
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public CountryRegionDto CountryRegion { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Person.StateProvince, StateProvinceDto>();
        }
    }
}