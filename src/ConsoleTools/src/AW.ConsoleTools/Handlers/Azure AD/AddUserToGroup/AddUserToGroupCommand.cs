using MediatR;

namespace AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup
{
    public class AddUserToGroupCommand : IRequest
    {
        public AddUserToGroupCommand(string userName, string groupName, string groupId)
        {
            UserName = userName;
            GroupName = groupName;
            GroupId = groupId;
        }

        public string UserName { get; init; }
        public string GroupName { get; init; }
        public string GroupId { get; init; }
    }
}