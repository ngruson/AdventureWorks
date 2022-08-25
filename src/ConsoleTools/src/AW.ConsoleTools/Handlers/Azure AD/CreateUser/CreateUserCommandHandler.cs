using AutoMapper;
using AW.ConsoleTools.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace AW.ConsoleTools.Handlers.AzureAD.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly GraphServiceClient _client;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger,
            GraphServiceClient client,
            IMapper mapper,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _client = client;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating user {DisplayName}", request.DisplayName);

            var user = new Microsoft.Graph.User
            {
                AccountEnabled = true,
                DisplayName = request.DisplayName,
                MailNickname = request.MailNickname,
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = false,
                    Password = _configuration["DefaultUserPassword"]
                },
                UserPrincipalName = $"{request.MailNickname.NormalizeExt()}@{_configuration["Domain"]}"
            };

            var result = await _client.Users.Request().AddAsync(user, cancellationToken);

            _logger.LogInformation("Returning user {DisplayName}", request.DisplayName);
            return _mapper.Map<User>(result);
        }
    }
}