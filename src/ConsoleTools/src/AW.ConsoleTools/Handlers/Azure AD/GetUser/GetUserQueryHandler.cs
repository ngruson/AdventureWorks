using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace AW.ConsoleTools.Handlers.AzureAD.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User?>
    {
        private readonly ILogger<GetUserQueryHandler> _logger;
        private readonly GraphServiceClient _client;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(
            ILogger<GetUserQueryHandler> logger,
            GraphServiceClient client,
            IMapper mapper
        )
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }

        public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting Azure AD user {UserName}", request.UserName);

            var usersResponse = await _client.Users
                .GetAsync(reqInfo =>
                    {
                        reqInfo.QueryParameters.Expand = new[] { "memberOf" };
                        reqInfo.QueryParameters.Filter = $"displayName eq '{request.UserName.Replace("'", "''")}'";
                    },
                    cancellationToken
                );

            var user = usersResponse!.Value!.FirstOrDefault(_ => _.DisplayName == request.UserName);
            
            if (user == null)
            {
                _logger.LogInformation("Azure AD user {DisplayName} not found", request.UserName);                
                return null;
            }
            _logger.LogInformation("Found Azure AD user {@User}", user);

            var result = _mapper.Map<User?>(user);

            _logger.LogInformation("Returning Azure AD user {@User}", result);
            return result;
        }
    }
}
