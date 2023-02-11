using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.Idempotency;
using AW.Services.Infrastructure.EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.Sales.Core.Handlers.Identified
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R?>, R?>
        where T : IRequest<R?>
    {
        private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;

        public IdentifiedCommandHandler(
            ILogger<IdentifiedCommandHandler<T, R>> logger,
            IMediator mediator,
            IRequestManager requestManager
        ) => (_logger, _mediator, _requestManager) = (logger, mediator, requestManager);

        /// <summary>
        /// Creates the result value to return if a previous request was found
        /// </summary>
        /// <returns></returns>
        protected virtual R? CreateResultForDuplicateRequest()
        {
            return default;
        }

        /// <summary>
        /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
        /// just enqueues the original inner command.
        /// </summary>
        /// <param name="request">IdentifiedCommand which contains both original command & request ID</param>
        /// <returns>Return value of inner command or default value if request same ID was found</returns>
        public async Task<R?> Handle(IdentifiedCommand<T, R?> request, CancellationToken cancellationToken)
        {
            var alreadyExists = await _requestManager.ExistAsync(request.Id);
            if (alreadyExists)
            {
                _logger.LogInformation("----- Request {Id} already exists", request.Id);
                return CreateResultForDuplicateRequest();
            }
            else
            {
                _logger.LogInformation("----- Creating request {Id}", request.Id);
                await _requestManager.CreateRequestForCommandAsync<T>(request.Id);
                try
                {
                    var command = request.Command;
                    var commandName = command.GetGenericTypeName();
                    var idProperty = string.Empty;
                    var commandId = string.Empty;

                    switch (command)
                    {
                        case CreateSalesOrderCommand createSalesOrderCommand:
                            idProperty = nameof(createSalesOrderCommand.UserId);
                            commandId = createSalesOrderCommand.UserId;
                            break;

                        default:
                            idProperty = "Id?";
                            commandId = "n/a";
                            break;
                    }

                    _logger.LogInformation(
                        "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        commandName,
                        idProperty,
                        commandId,
                        command);

                    // Send the embedded business command to mediator so it runs its related CommandHandler 
                    var result = await _mediator.Send(command, cancellationToken);

                    _logger.LogInformation(
                        "----- Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        result,
                        commandName,
                        idProperty,
                        commandId,
                        command);

                    return result;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Exception occurred: {Message}, {StackTrace}", ex.Message, ex.StackTrace);
                    return default;
                }
            }
        }
    }
}