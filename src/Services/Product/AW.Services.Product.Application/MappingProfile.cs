﻿using AW.Services.Product.Application.Common.AutoMapper;
using System.Reflection;

namespace AW.Services.Product.Application
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile() : base()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}