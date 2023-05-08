﻿using AW.SharedKernel.AutoMapper;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class Shift : IMapFrom<Entities.Shift>
    {
        public string? Name { get; set; }
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}