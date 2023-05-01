using AutoMapper;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AW.Services.HumanResources.Core.Handlers.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Department>
    {
        private readonly ILogger<CreateDepartmentCommandHandler> _logger;
        private readonly IRepository<Entities.Department> _departmentRepository;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(
            ILogger<CreateDepartmentCommandHandler> logger,
            IRepository<Entities.Department> departmentRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Department> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle called");

            _logger.LogInformation("Saving new department to database");
            var department = _mapper.Map<Entities.Department>(request.Department);
            department = await _departmentRepository.AddAsync(department, cancellationToken);

            _logger.LogInformation("Returning department");
            return _mapper.Map<Department>(department);
        }
    }
}
