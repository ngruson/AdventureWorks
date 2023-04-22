﻿using MediatR;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.DeleteDepartmentHistory
{
    public class DeleteDepartmentHistoryCommand : IRequest
    {
        public string? LoginID { get; set; }
        public string? DepartmentName { get; set; }
        public string? ShiftName { get; set; }
        public DateTime StartDate { get; set; }
    }
}
