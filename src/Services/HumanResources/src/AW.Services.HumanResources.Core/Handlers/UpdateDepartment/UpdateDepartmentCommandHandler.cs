using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.HumanResources.Core.GuardClauses;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<Department>>
    {
        private readonly ILogger<UpdateDepartmentCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateDepartmentCommand> _validator;

        public UpdateDepartmentCommandHandler(
            ILogger<UpdateDepartmentCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository,
            IMapper mapper,
            IValidator<UpdateDepartmentCommand> validator
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Department>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
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
                var spec = new GetDepartmentSpecification(request.Department.ObjectId);
                var department = await _departmentRepository.SingleOrDefaultAsync(spec, cancellationToken);
                var result = Guard.Against.DepartmentNull(department, request.Department.ObjectId, _logger);
                if (!result.IsSuccess)
                    return result;

                _logger.LogInformation("Updating department");
                _mapper.Map(request.Department, department);

                _logger.LogInformation("Saving department to database");
                await _departmentRepository.UpdateAsync(department!, cancellationToken);

                _logger.LogInformation("Returning department");
                return Result.Success(
                    _mapper.Map<Department>(department)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                return Result.Error(ex.Message);
            }
        }
    }
}
