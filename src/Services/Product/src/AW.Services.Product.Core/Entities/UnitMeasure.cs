using AW.Services.SharedKernel.Interfaces;
using System;

namespace AW.Services.Product.Core.Entities
{
    public class UnitMeasure : IAggregateRoot
    {
        public string UnitMeasureCode { get; private set; }
        public string Name { get; private set; }

        public DateTime ModifiedDate { get; private set; }
    }
}