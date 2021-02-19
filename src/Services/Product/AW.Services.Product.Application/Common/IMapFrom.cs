﻿using AutoMapper;

namespace AW.Services.Product.Application.Common
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}