﻿using AutoMapper;
using AW.Services.Product.REST.API;
using Xunit;

namespace AW.Services.Product.WCF.UnitTests
{
    public class AutoMapperUnitTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}