﻿namespace AW.UI.Web.SharedKernel.Interfaces.Api
{
    public interface IDepartmentApiClient
    {
        Task<List<Department.Handlers.GetDepartments.Department>?> GetDepartments();
    }
}
