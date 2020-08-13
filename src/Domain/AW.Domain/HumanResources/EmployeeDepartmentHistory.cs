using System;

namespace AW.Domain.HumanResources
{
    public class EmployeeDepartmentHistory : BaseEntity
    {
        public short DepartmentID { get; set; }

        public byte ShiftID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Shift Shift { get; set; }
    }
}