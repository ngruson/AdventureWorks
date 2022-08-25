using MediatR;

namespace AW.ConsoleTools.Handlers.AzureAD.GetGroup
{
    public class GetGroupQuery : IRequest<Group>
    {
        public GetGroupQuery(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; init; }
    }
}