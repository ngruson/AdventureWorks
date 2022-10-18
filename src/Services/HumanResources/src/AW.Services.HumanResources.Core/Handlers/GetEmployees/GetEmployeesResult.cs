namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class GetEmployeesResult
    {
        public GetEmployeesResult(List<Employee> employees, int totalEmployees)
        {
            Employees = employees;
            TotalEmployees = totalEmployees;
        }

        public List<Employee> Employees { get; init; }
        public int TotalEmployees { get; init; }
    }
}