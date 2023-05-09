﻿using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<Result<Employee>>
    {
        public UpdateEmployeeCommand(string key, Employee employee)
        {
            Key = key;
            Employee = employee;
        }

        public string Key { get; set; }
        public Employee? Employee { get; set; }
    }
}
