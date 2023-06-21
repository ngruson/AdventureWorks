using AW.ConsoleTools.Handlers.AzureAD.CreateUser;
using AW.ConsoleTools.Handlers.AzureAD.GetGroup;
using AW.ConsoleTools.Handlers.AzureAD.GetUser;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using AW.SharedKernel.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup;
using Ardalis.GuardClauses;
using AW.Services.HumanResources.Core.GuardClauses;

namespace AW.ConsoleTools.Handlers.AzureAD.CreateUsers
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand>
    {
        private readonly ILogger<CreateUsersCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateUsersCommandHandler(
            ILogger<CreateUsersCommandHandler> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetEmployeesQuery(),
                cancellationToken
            );
            Guard.Against.Null(result.Value);
            
            if (result.IsSuccess)
            {
                foreach (var employee in result.Value)
                {
                    var groupName = employee.DepartmentHistory?[0].Department?.GroupName;

                    var group = await _mediator.Send(
                        new GetGroupQuery(groupName!),
                        cancellationToken
                    );
                    Guard.Against.Null(group, _logger);

                    var user = await _mediator.Send(
                        new GetUserQuery(employee?.Name?.FullName!),
                        cancellationToken
                    );

                    if (user == null)
                    {
                        _logger.LogInformation("Creating user {DisplayName}", employee?.Name?.FullName);

                        await _mediator.Send(
                            new CreateUserCommand(
                                employee?.Name?.FullName!,
                                employee?.LoginID!
                            ),
                            cancellationToken
                        );
                        _logger.LogInformation("Succesfully created user {DisplayName}", employee?.Name?.FullName);

                        _logger.LogInformation("Getting user {DisplayName}", employee?.Name?.FullName);
                        user = await _mediator.Send(
                            new GetUserQuery(employee?.Name?.FullName!),
                            cancellationToken
                        );
                        Guard.Against.Null(user, _logger);
                    }
                    else
                        _logger.LogInformation("User {DisplayName} already exists", employee?.Name?.FullName);

                    if (user?.MemberOf?.Find(_ => _.Id == group.Id) == null)
                    {
                        await _mediator.Send(new AddUserToGroupCommand(
                                user?.DisplayName!,
                                group.DisplayName!,
                                group.Id!
                            ),
                            cancellationToken
                        );
                    }
                }
            }
        }
    }
}
