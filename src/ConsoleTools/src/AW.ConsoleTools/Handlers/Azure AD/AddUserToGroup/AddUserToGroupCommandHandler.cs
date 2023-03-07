using Ardalis.GuardClauses;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup
{
    public class AddUserToGroupCommandHandler : IRequestHandler<AddUserToGroupCommand>
    {
        private readonly ILogger<AddUserToGroupCommandHandler> _logger;
        private readonly GraphServiceClient _client;

        public AddUserToGroupCommandHandler(
            ILogger<AddUserToGroupCommandHandler> logger,
            GraphServiceClient client
        )
        {
            _logger = logger;
            _client = client;            
        }

        public async Task<Unit> Handle(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding user {UserName} to group {GroupName}", request.UserName, request.GroupName);

            var user = await GetUser(request, cancellationToken);

            var userRef = new ReferenceCreate
            {
                OdataId = user?.Id
            };

            await _client.Groups[request.GroupId].Members
                .Ref                
                .PostAsync(userRef, null, cancellationToken);

            _logger.LogInformation("Succesfully added user {UserName} to group {GroupName}", request.UserName, request.GroupName);

            return Unit.Value;
        }

        private async Task<User?> GetUser(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var usersResponse = await _client.Users
                .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters.Expand = new[] { "memberOf" };
                        requestConfiguration.QueryParameters.Filter = $"displayName eq '{request.UserName?.Replace("'", "''")}'";
                    },                    
                    cancellationToken
                );

            var user = usersResponse!.Value!.FirstOrDefault(_ => _.DisplayName == request.UserName);
            Guard.Against.Null(user, _logger);

            return user;
        }
    }
}
