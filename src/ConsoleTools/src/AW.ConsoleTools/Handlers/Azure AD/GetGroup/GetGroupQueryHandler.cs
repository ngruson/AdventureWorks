using Ardalis.GuardClauses;
using AutoMapper;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace AW.ConsoleTools.Handlers.AzureAD.GetGroup
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, Group>
    {
        private readonly ILogger<GetGroupQueryHandler> _logger;
        private readonly GraphServiceClient _client;
        private readonly IMapper _mapper;

        public GetGroupQueryHandler(
            ILogger<GetGroupQueryHandler> logger,
            GraphServiceClient client,
            IMapper mapper
        )
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }

        public async Task<Group> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting Azure AD group {GroupName}", request.GroupName);

            var groupsResponse = await _client.Groups
                    .Request()
                    .Expand(_ => _.Members)
                    .Filter($"displayName eq '{request.GroupName}'")
                    .GetAsync(cancellationToken);

            var group = groupsResponse.FirstOrDefault(_ => _.DisplayName == request.GroupName);
            Guard.Against.Null(group, _logger);
            
            var result = _mapper.Map<Group>(group);

            _logger.LogInformation("Returning Azure AD group {@Group}", group);
            return result;
        }
    }
}