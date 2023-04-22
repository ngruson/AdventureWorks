namespace AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployee
{
    public class EmployeeDepartmentHistory
    {
        public Department? Department { get; set; }
        public Shift? Shift { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
