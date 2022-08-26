using AW.ConsoleTools.Handlers.AzureAD.CreateUser;
using AW.ConsoleTools.Handlers.AzureAD.GetGroup;
using AW.ConsoleTools.Handlers.AzureAD.GetUser;
using AW.Services.HumanResources.Core.Handlers.GetAllEmployees;
using MediatR;
using Microsoft.Extensions.Logging;
using AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup;
using Ardalis.GuardClauses;

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

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var employees = await _mediator.Send(
                new GetAllEmployeesQuery(),
                cancellationToken
            );
            Guard.Against.Null(employees, nameof(employees));

            foreach (var employee in employees)
            {
                var groupName = employee.DepartmentHistory?.First().Department?.GroupName;
                
                var group = await _mediator.Send(
                    new GetGroupQuery(groupName!),
                    cancellationToken
                );
                Guard.Against.Null(group, nameof(group));

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
                    Guard.Against.Null(user, nameof(user));
                }
                else
                    _logger.LogInformation("User {DisplayName} already exists", employee?.Name?.FullName);

                if (user?.MemberOf?.FirstOrDefault(_ => _.Id == group.Id) == null)
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

            return Unit.Value;
        }
    }
}