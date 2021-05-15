using AutoMapper;

namespace AW.Services.Product.Application.Common.AutoMapper
{
    public interface IMapFrom<T>
    {
        #if NETSTANDARD2_0
        void Mapping(Profile profile);
        # else
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
        #endif
    }
}