namespace AW.Services.HumanResources.Core.Entities
{
    public class EmployeeDepartmentHistory
    {
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public int ShiftID { get; set; }
        public Department? Department { get; set; }
        public Shift? Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}