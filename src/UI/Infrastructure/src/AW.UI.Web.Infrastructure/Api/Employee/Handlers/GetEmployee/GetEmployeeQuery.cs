﻿using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployee
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public GetEmployeeQuery(string? loginID)
        {
            LoginID = loginID;
        }
        public string? LoginID { get; set; }
    }
}