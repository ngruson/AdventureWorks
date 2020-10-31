using System;

namespace AW.Domain.HumanResources
{
    public class Shift
    {
        public virtual int Id { get; protected set; }
        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}