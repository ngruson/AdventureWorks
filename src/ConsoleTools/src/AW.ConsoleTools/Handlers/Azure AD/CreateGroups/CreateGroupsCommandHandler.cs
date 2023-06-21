using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AW.ConsoleTools.Handlers.AzureAD.CreateGroups
{
    public class CreateGroupsCommandHandler : IRequestHandler<CreateGroupsCommand>
    {
        private readonly ILogger<CreateGroupsCommandHandler> _logger;
        private readonly GraphServiceClient _client;
        public CreateGroupsCommandHandler(
            ILogger<CreateGroupsCommandHandler> logger, 
            GraphServiceClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Handle(CreateGroupsCommand request, CancellationToken cancellationToken)
        {
            await CreateGroups(cancellationToken);
        }

        private async Task<GroupCollectionResponse?> CreateGroups(CancellationToken cancellationToken)
        {
            var groupNames = GetGroupList();
            var groups = await _client.Groups
                .GetAsync(requestConfiguration =>
                    requestConfiguration.QueryParameters.Orderby = new[] { "displayName" },
                    cancellationToken
                );

            groupNames.ForEach(async groupName =>
            {
                if (!groups!.Value!.Exists(group => group.DisplayName == groupName))
                {
                    _logger.LogInformation("Creating AAD group {GroupName}", groupName);

                    var newGroup = await _client.Groups.PostAsync(
                        new Group
                        {
                            DisplayName = groupName,
                            MailEnabled = false,
                            MailNickname = groupName.Replace(" ", "-"),
                            SecurityEnabled = true
                        }
                    );

                    groups.Value!.Add(newGroup!);
                }
                else
                {
                    _logger.LogInformation("AAD group {GroupName} already exists", groupName);
                }
            });

            return groups;
        }

        private static List<string> GetGroupList()
        {
            return new List<string>
            {
                "Executive General and Administration",
                "Inventory Management",
                "Manufacturing",
                "Quality Assurance",
                "Research and Development",
                "Sales and Marketing"
            };
        }
    }
}
