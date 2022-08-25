using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

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

        public async Task<Unit> Handle(CreateGroupsCommand request, CancellationToken cancellationToken)
        {
            await CreateGroups(cancellationToken);

            return Unit.Value;
        }

        private async Task<IGraphServiceGroupsCollectionPage> CreateGroups(CancellationToken cancellationToken)
        {
            var groupNames = GetGroupList();
            var groups = await _client.Groups.Request()
                .OrderBy("displayName")
                .GetAsync(cancellationToken);

            groupNames.ForEach(async groupName =>
            {
                if (!groups.Any(group => group.DisplayName == groupName))
                {
                    _logger.LogInformation("Creating AAD group {GroupName}", groupName);

                    var newGroup = await _client.Groups.Request().AddAsync(
                        new Group
                        {
                            DisplayName = groupName,
                            MailEnabled = false,
                            MailNickname = groupName.Replace(" ", "-"),
                            SecurityEnabled = true
                        },
                        cancellationToken
                    );

                    groups.Add(newGroup);
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