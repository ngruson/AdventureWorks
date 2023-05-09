using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.DeleteDepartment
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result>
    {
        private readonly ILogger<DeleteDepartmentCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IValidator<DeleteDepartmentCommand> _validator;

        public DeleteDepartmentCommandHandler(
            ILogger<DeleteDepartmentCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository,
            IValidator<DeleteDepartmentCommand> validator
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _validator = validator;
        }

        public async Task<Result> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Getting department from database");
                var spec = new GetDepartmentSpecification(request.ObjectId);
                var department = await _departmentRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.DepartmentNull(department, request.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Deleting department from database");
                await _departmentRepository.DeleteAsync(department!, cancellationToken);

                _logger.LogInformation("Deleted department from database");

                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
