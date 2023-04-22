﻿using AW.Services.HumanResources.Core.Entities;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class EmployeeDepartmentHistory : IMapFrom<Entities.EmployeeDepartmentHistory>
    {
        public Department? Department { get; set; }
        public Shift? Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
