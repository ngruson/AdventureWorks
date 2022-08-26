using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.ConsoleTools.Handlers.AzureAD.GetUser
{
    public class User : IMapFrom<Microsoft.Graph.User>
    {
        public User() { }

        public string? Id { get; init; }
        public string? DisplayName { get; init; }
        public List<Group>? MemberOf { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Microsoft.Graph.User, User>()
                .ForMember(_ => _.MemberOf, opt => opt.MapFrom((src, dest, destProp, ctx) =>
                    ctx.Mapper.Map<List<Group>>(src.MemberOf)));
        }
    }
}