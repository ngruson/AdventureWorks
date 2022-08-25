using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.ConsoleTools.Handlers.AzureAD.CreateUser
{
    public class User : IMapFrom<GetUser.User>
    {
        public string? Id { get; init; }
        public string? DisplayName { get; init; }
        public List<Group>? MemberOf { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetUser.User, User>();
            profile.CreateMap<Microsoft.Graph.User, User>()
                .ForMember(_ => _.MemberOf, opt => opt.Ignore());
        }
    }
}