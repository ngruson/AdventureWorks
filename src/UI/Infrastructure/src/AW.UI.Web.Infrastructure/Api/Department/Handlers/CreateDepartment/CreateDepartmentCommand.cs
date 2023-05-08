﻿using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<Department>
    {
        public CreateDepartmentCommand(Department department)
        {
            Department = department;
        }
        public Department Department { get; set; }
    }
}