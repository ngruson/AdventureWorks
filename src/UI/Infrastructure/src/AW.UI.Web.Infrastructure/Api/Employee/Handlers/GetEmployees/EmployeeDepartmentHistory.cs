﻿namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees
{
    public class EmployeeDepartmentHistory
    {
        public Department? Department { get; set; }
        public Shift? Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}