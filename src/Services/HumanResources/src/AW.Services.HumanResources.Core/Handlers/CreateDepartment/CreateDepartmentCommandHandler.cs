using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<CreatedDepartment>>
    {
        private readonly ILogger<CreateDepartmentCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDepartmentCommand> _validator;

        public CreateDepartmentCommandHandler(
            ILogger<CreateDepartmentCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository,
            IMapper mapper,
            IValidator<CreateDepartmentCommand> validator
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<CreatedDepartment>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validating command");

                var validation = await _validator.ValidateAsync(request, cancellationToken);
                if (!validation.IsValid)
                {
                    return Result.Invalid(validation.AsErrors());
                }

                _logger.LogInformation("Saving new department to database");
                var department = _mapper.Map<Entities.Department>(request.Department);
                department = await _departmentRepository.AddAsync(department, cancellationToken);

                _logger.LogInformation("Returning department");
                return Result.Success(
                    _mapper.Map<CreatedDepartment>(department)
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
