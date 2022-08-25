using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.ConsoleTools.Handlers.AzureAD.CreateUser
{
    public class Group : IMapFrom<GetUser.Group>
    {
        public string? Id { get; init; }
        public string? DisplayName { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetUser.Group, Group>();
            profile.CreateMap<Microsoft.Graph.Group, Group>();
        }
    }
}