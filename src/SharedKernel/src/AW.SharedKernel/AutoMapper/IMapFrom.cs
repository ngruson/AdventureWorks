using AutoMapper;

namespace AW.SharedKernel.AutoMapper
{
    public interface IMapFrom<T>
    {
        #if NETSTANDARD2_0
        void Mapping(Profile profile);
        #elif NETSTANDARD2_1
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
        #endif
    }
}